using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Algorithm
{
    public class AngularBoundaryRasterDetector
    {
        public StlAbutment Abutment { get; set; }
        public int NRotSteps { get; set; }
        public float DeltaTheta { get; set; }
        public List<Facet> Facets { get; set; }

        public AngularBoundaryRasterDetector(StlAbutment abutment, int nRotSteps)
        {
            Abutment = abutment;
            NRotSteps = nRotSteps;
            DeltaTheta = (float) (2*Math.PI/NRotSteps);
            Facets = abutment.ShellFacets.ToList();
        }

        public Dictionary<float, List<Vertex>> GetBoundaries(bool doFiltering)
        {
            var facetsByTheta = GetFacetsByTheta();
        }

        private Dictionary<float, List<Facet>> GetFacetsByTheta()
        {
            var facetsByAngularLimit = GetFacetsByAngularLimit();
            var facetsWithTheta = GetFacetsWithTheta(facetsByAngularLimit);
            return facetsWithTheta.GroupBy(pair => pair.Item1)
                .ToDictionary(g => g.Key,
                              g => g.Select(_ => _.Item2).OrderBy(_ => _.MinZ).ToList());
        }

        private List<Tuple<float, float, Facet>> GetFacetsByAngularLimit()
        {
            return (from facet in Facets
                    let angularLimits = GetFacetAngularLimits(facet)
                    select new Tuple<float, float, Facet>(angularLimits.Item1,
                                                          angularLimits.Item2,
                                                          facet)).ToList();
        }

        private IEnumerable<Tuple<float, Facet>> GetFacetsWithTheta(
            List<Tuple<float, float, Facet>> facetsByAngularLimit)
        {
            return (from step in Enumerable.Range(0, NRotSteps)
                    select step*DeltaTheta
                    into theta
                    from trio in facetsByAngularLimit
                    where theta >= trio.Item1 && theta <= trio.Item2
                    select new Tuple<float, Facet>(theta, trio.Item3)).ToList();
        }

        private Tuple<float, float> GetFacetAngularLimits(Facet facet)
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
                results.Add((float) -(2*Math.PI - t2));
            }
            return new Tuple<float, float>(results.Min(), results.Max());
        }
    }
}