using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Algorithm
{
    public class RevBoundaryDetector
    {
        public int NRotSteps { get; set; }
        public int NVertSteps { get; set; }
        public float ThetaDelta { get; set; }
        public float VertDelta { get; set; }
        public List<Facet> Facets { get; set; }
    
        public RevBoundaryDetector(StlAbutment abutment, int nRotSteps, int nVertSteps)
        {
            NRotSteps = nRotSteps;
            NVertSteps = nVertSteps;
            ThetaDelta = (float) (2 * Math.PI / NRotSteps);
            VertDelta = (float) (abutment.MaxZ/NVertSteps);
            Facets = new FacetGrouper(abutment).GetOutwardsFacets().Except(abutment.AbutmentBase.Facets).ToList();
        }

        public RevBoundary GetRevolutionBoundary()
        {
            var result = new Dictionary<int, List<Vertex>>();
            for (int rotStep = 0; rotStep < NRotSteps; rotStep++)
            {
                var theta = rotStep*ThetaDelta + ThetaDelta/2;
                var ps = GetRevolutionBoundaryForTheta(theta);
                result.Add(rotStep, ps);
            }
/*
            result = (from rotStep in Enumerable.Range(0, NRotSteps)
                      let theta = rotStep*ThetaDelta
                      select new
                      {
                          rotStep,
                          L =
                              (from vertStep in Enumerable.Range(0, NVertSteps)
                               let z = vertStep*VertDelta
                               let ray = new Vector3((float) Math.Cos(theta), (float) Math.Sin(theta), z)
                               let vertex = (from facet in Facets
                                             select facet.Intersects(new LineSegment(new Vertex(0, 0, z),
                                                                                     new Vertex(Vector3.Multiply(ray, 1000F)))))
                                   .FirstOrDefault()
                               where vertex != null
                               select vertex).ToList()
                      }).ToDictionary(_ => _.rotStep, _ => _.L);
 */ 
            return new RevBoundary {Boundaries = result};
        }

        private List<Vertex> GetRevolutionBoundaryForTheta(float theta)
        {
            var facets = Facets.Where(_ => HasTheta(_, theta)).OrderBy(_ => _.Center.Z).ToList();
            if (!facets.Any())
                return null;
            float minZ = float.MaxValue;
            float minZR = float.MaxValue;
            float maxZ = float.MinValue;
            float maxZR = float.MinValue;
            float maxR = float.MinValue;
            bool maxRFound = false;
            float maxRZ = float.MinValue;
            Facet maxZFacet = null;
            Facet minZFacet = null;
            Facet maxRFacet = null;
            Vertex maxZVertex = null;
            Vertex minZVertex = null;
            Vertex maxRVertex = null;
            foreach (var facet in facets)
            {
                foreach (var v in facet.Vertices)
                {
                    var z = v.Z;
                    var x = v.X;
                    var y = v.Y;
                    var r = (float)Math.Sqrt(x * x + y * y);

                    if (minZ > z)
                    {
                        minZ = z;
                        minZR = r;
                        minZFacet = facet;
                        minZVertex = v;
                    }
                    if (maxZ < z)
                    {
                        maxZ = z;
                        maxZR = r;
                        maxZFacet = facet;
                        maxZVertex = v;
                    }
                    if (!maxRFound)
                    {
                        if (maxR < r)
                        {
                            maxR = r;
                            maxRZ = z;
                            maxRFacet = facet;
                            maxRVertex = v;
                        }
                        else
                        {
                            maxRFound = r < maxR*0.95;
                        }
                    }
                }
            }
            var cos = (float)Math.Cos(theta);
            var sin = (float)Math.Sin(theta);
            var bottom = new Vertex(cos * minZR, sin * minZR, 0);
            var top = new Vertex(cos * maxZR, sin * maxZR, maxZ);
            var fat = new Vertex(cos * maxR, sin * maxR, maxRZ);
            var ps = new List<Vertex> {bottom, fat, top};

            var vertSteps = new float[3];
            vertSteps[0] = maxRZ/2;
            vertSteps[1] = (maxZ - maxRZ) / 3 + maxRZ;
            vertSteps[2] = (maxZ - maxRZ) * 0.67F + maxRZ;

            foreach (var z in vertSteps)
            {
                var x = cos*1000;
                var y = sin*1000;
                var ray = new Vertex(x, y, z);
                foreach (var facet in facets)
                {
                    Vertex p = facet.Intersects(new LineSegment(new Vertex(0, 0, z), ray));
                    if (p != null)
                    {
                        ps.Add(p);
                        break;
                    }
                }
            }
            return ps.OrderBy(_ => _.Z).ToList();
        }

        private bool HasTheta(Facet facet, float theta)
        {
            var angleLimits = GetAngleLimits(facet);
            return angleLimits.First() <= theta && angleLimits.Last() >= theta;
        }

/*
        private List<Boundary> GetRByVertStep(IEnumerable<Facet> facets, int rotStep, float theta)
        {
            var result =
                (from i in Enumerable.Range(0, NVertSteps)
                 let z = (float) (i*VertDelta)
                 let boundary = (from facet in facets
                                 where z >= facet.MinZ && z <= facet.MaxZ
                                 let center = facet.Center
                                 let x = center.X
                                 let y = center.Y
                                 let r = (float) Math.Sqrt(x*x + y*y)
                                 select new Boundary {RotStep = rotStep, R = r, Z = z, Theta = theta}).FirstOrDefault()
                 where boundary != null
                 select boundary).ToList();
            return result;
        }

        private Dictionary<int, List<Facet>> GroupFacetsByAngle()
        {
            var bag = Enumerable.Range(0, NRotSteps).ToDictionary(_ => _, _ => new List<Facet>());

            foreach (var facet in Facets)
            {
                var angleLimits = GetAngleLimits(facet);
                var min = (int)(angleLimits.First() / ThetaDelta);
                var max = (int)(angleLimits.Last() / ThetaDelta);
                for (int i = min; i <= max; i++)
                {
                    bag[i].Add(facet);
                }
            }
            return bag;
        }
*/
        private double[] GetAngleLimits(Facet facet)
        {
            return facet.Vertices.Select(GetAngle).OrderBy(_ => _).ToArray();
        }

        private double GetAngle(Vertex vertex)
        {
            var x = vertex.X;
            var y = vertex.Y;
            var theta = Math.Acos(x/Math.Sqrt(x*x + y*y))*(y < 0 ? -1 : 1);
            if (theta < 0)
                theta += 2*Math.PI;
            return theta;
        }
    }
}
