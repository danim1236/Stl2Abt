using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;

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
                 select new
                 {
                     Theta = theta,
                     Facets =
                         facets.SelectMany(_ => _.Intersects(plane))
                             .OrderBy(_ => _.Z)
                             .ToList()
                 }).ToDictionary(_ => _.Theta, _ => _.Facets);
            return verticesByTheta;
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
