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
        public double DeltaStep { get; set; }
        public double VertDelta { get; set; }
        public List<Facet> Facets { get; set; }
    
        public RevBoundaryDetector(StlAbutment abutment, int nRotSteps, int nVertSteps)
        {
            NRotSteps = nRotSteps;
            NVertSteps = nVertSteps;
            DeltaStep = 2 * Math.PI / NRotSteps;
            VertDelta = abutment.MaxZ/NVertSteps;
            Facets = new FacetGrouper(abutment).GetOutwardsFacets().Except(abutment.AbutmentBase.Facets).ToList();
        }

        public RevBoundary GetRevolutionBoundary()
        {
            var facetsByRotStep = GroupFacetsByAngle();
            var result = new RevBoundary();
            for (int rotStep = 0; rotStep < NRotSteps; rotStep++)
            {
                result.Boundaries[rotStep] = GetRByVertStep(facetsByRotStep[rotStep]);
            }
            return result;
        }

        private AngularBoundary GetRByVertStep(List<Facet> facets)
        {
            var result = new AngularBoundary();
            for (int i = 0; i < NVertSteps; i++)
            {
                var z = (float)(i*VertDelta);
                foreach (var facet in facets)
                {
                    if (z >= facet.MinZ && z <= facet.MaxZ)
                    {
                        var center = facet.Center;
                        var x = center.X;
                        var y = center.Y;
                        result.RadiusAndZ.Add(new Tuple<float, float>((float) Math.Sqrt(x*x + y*y), z));
                    }
                }
            }
            return result;
        }
        
        private Dictionary<int, List<Facet>> GroupFacetsByAngle()
        {
            var bag = Enumerable.Range(0, NRotSteps).ToDictionary(_ => _, _ => new List<Facet>());

            foreach (var facet in Facets)
            {
                var angleLimits = GetAngleLimits(facet);
                var min = (int)(angleLimits.First() / DeltaStep);
                var max = (int)(angleLimits.Last() / DeltaStep);
                for (int i = min; i <= max; i++)
                {
                    bag[i].Add(facet);
                }
            }
            return bag;
        }

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
