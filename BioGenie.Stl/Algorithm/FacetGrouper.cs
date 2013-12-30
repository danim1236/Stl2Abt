using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using BioGenie.Stl.Tools;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public class FacetGrouper
    {
        public StlDocument StlDocument { get; set; }

        public FacetGrouper(StlDocument document)
        {
            StlDocument = document;
        }

        public List<FacetsGroup> GroupByNormal(float error)
        {
            return Math.Abs(error) < 1E-3 ? GroupByNormalStrict() : GroupByNormalWithError(error);
        }

        private List<FacetsGroup> GroupByNormalStrict()
        {
            var bag = new Dictionary<Normal, List<Facet>>();
            foreach (var facet in StlDocument.Facets)
            {
                var normal = facet.Normal;
                if (!bag.ContainsKey(normal))
                    bag[normal] = new List<Facet> {facet};
                else
                    bag[normal].Add(facet);
            }
            return (from g in bag
                select new FacetsGroup
                {
                    Normal = g.Key,
                    Facets = g.Value,
                }).ToList();
        }

        private List<FacetsGroup> GroupByNormalWithError(float error)
        {
            var tol = 1 - error;
            var bag = new HashSet<Tuple<Vertex, List<Facet>>>();
            foreach (var facet in StlDocument.Facets)
            {
                var normal = facet.Normal;
                bool found = false;
                foreach (var tuple in bag)
                {
                    if (Vector3.Dot(normal.ToVector3(), tuple.Item1.ToVector3()) >= tol)
                    {
                        var tFacets = tuple.Item2;
                        tFacets.Add(facet);
                        bag.Remove(tuple);
                        bag.Add(new Tuple<Vertex, List<Facet>>(
                            tFacets.Select(_ => _.Normal).Mean(),
                            tFacets));
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    bag.Add(new Tuple<Vertex, List<Facet>>(facet.Normal, new List<Facet> {facet}));
                }
            }
            return (from g in bag
                    select new FacetsGroup
                    {
                        Normal = new Normal(g.Item1),
                        Facets = g.Item2,
                    }).ToList();
        }
    }
}
