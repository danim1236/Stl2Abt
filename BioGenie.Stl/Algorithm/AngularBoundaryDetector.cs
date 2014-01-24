using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public class AngularBoundaryDetector
    {
        public StlAbutment Abutment { get; set; }
        public int NRotSteps { get; set; }
        public float DeltaTheta { get; set; }
        public List<Facet> Facets { get; set; }

        public AngularBoundaryDetector(StlAbutment abutment, int nRotSteps)
        {
            Abutment = abutment;
            NRotSteps = nRotSteps;
            DeltaTheta = (float) (2*Math.PI/NRotSteps);
            Facets = abutment.ShellFacets.ToList();
        }

        public Dictionary<float, List<Vertex>> GetBoundaries()
        {
            var facetsByTheta = GetFacetsByTheta();
            Dictionary<float, List<Vertex>> verticesByTheta =
                (from pair in facetsByTheta
                 let theta = pair.Key
                 let facets = pair.Value
                 let plane = GetPlaneFromThetaZ(theta)
                 let vertices = facets.SelectMany(_ => _.Intersects(plane)).OrderBy(_ => _.Z).ToList()
                 select new
                 {
                     Theta = theta,
                     Vertices = CompactInZ(ExtendToXYPlane(CompactInZ(vertices)))
                 }).ToDictionary(_ => _.Theta, _ => _.Vertices);
            //var vs = verticesByTheta.OrderBy(_ => _.Key).ToList();
            //var a = CompactInZ(verticesByTheta.Values.Last());
            return verticesByTheta;
        }

        private List<Vertex> CompactInZ(List<Vertex> vertices)
        {
            var result = new List<Vertex>();
            var first = vertices.First();
            var accX = first.X;
            var accY = first.Y;
            var accCount = 1;
            var currentZ = first.Z;

            for (int i = 1; i < vertices.Count; i++)
            {
                var vertex = vertices[i];
                var z = vertex.Z;
                if (z - currentZ > 1E-3)
                {
                    result.Add(new Vertex(accX/accCount, accY/accCount, currentZ));
                    accX = 0;
                    accY = 0;
                    accCount = 0;
                    currentZ = z;
                }
                accX += vertex.X;
                accY += vertex.Y;
                ++accCount;
            }
            result.Add(new Vertex(accX/accCount, accY/accCount, currentZ));
            return result;
        }

        private List<Vertex> ExtendToXYPlane(List<Vertex> vertices)
        {
            int countBelow0 = 0;
            for (; countBelow0 < vertices.Count; countBelow0++)
            {
                if (vertices[countBelow0].Z >= 0)
                    break;
            }
            if (countBelow0 > 0)
                vertices = vertices.GetRange(countBelow0, vertices.Count - countBelow0);
            var p1 = vertices[0].ToVector3();
            var p2 = vertices[1].ToVector3();
            var dir = p2 - p1;
            var r = p2.Z/dir.Z;
            var p = p2 - Vector3.Multiply(dir, r);
            p.Z = 0;
            var vertex = new Vertex(p);
            if (float.IsNaN(vertex.X))
            {
                int abv = 1;
            }
               
            vertices.Insert(0, vertex);
            return vertices;
        }

        private Dictionary<float, List<Facet>> GetFacetsByTheta()
        {
            var facetsByAngularLimit = GetFacetsByAngularLimit();
            var facetsWithTheta = GetFacetsWithTheta(facetsByAngularLimit);
            return facetsWithTheta.GroupBy(pair => pair.Item1)
                .ToDictionary(g => g.Key,
                              g => g.Select(_ => _.Item2).OrderBy(_ => _.MinZ).ToList());
        }

        private IEnumerable<Tuple<float, Facet>> GetFacetsWithTheta(IEnumerable<Tuple<float, float, Facet>> facetsByAngularLimit)
        {
            return (from step in Enumerable.Range(0, NRotSteps)
                    select step*DeltaTheta
                    into theta
                    from trio in facetsByAngularLimit
                    where theta >= trio.Item1 && theta <= trio.Item2
                    select new Tuple<float, Facet>(theta, trio.Item3)).ToList();
        }

        private IEnumerable<Tuple<float, float, Facet>> GetFacetsByAngularLimit()
        {
            return (from facet in Facets
                    let angularLimits = GetFacetAngularLimits(facet)
                    select new Tuple<float, float, Facet>(angularLimits.Item1,
                                                          angularLimits.Item2,
                                                          facet)).ToList();
        }

        private Tuple<float,float> GetFacetAngularLimits(Facet facet)
        {
            var results = new List<float>();

            var ts = facet.Vertices.Select(_ => _.ThetaZ).ToList();
            var t1 = ts.Min();
            var t2 = ts.Max();
            if (t1 >= Math.PI || t2 - t1 <= Math.PI)
            {
                results.Add(t1);
                results.Add(t2);
            }
            else
            {
                results.Add(t1);
                results.Add((float)-(2 * Math.PI - t2));
            }
            return new Tuple<float, float>(results.Min(), results.Max());
        }

        private Plane GetPlaneFromThetaZ(float theta)
        {
            theta += (float)Math.PI/2;
            var normal = new Normal((float) Math.Cos(theta), (float) Math.Sin(theta), 0);
            return new Plane {Normal = normal, V0 = new Vertex(0, 0, 0)};

        }

    }
}
