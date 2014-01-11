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
        public static float AxisAlignmentThreshold = (float) 1E-3;

        public StlDocument StlDocument { get; set; }

        public FacetGrouper(StlDocument document)
        {
            StlDocument = document;
        }

        public List<FacetsGroup> GroupByNormal(float? error = null)
        {
            return error.HasValue ? GroupByNormalWithError(error.Value) : GroupByNormalStrict();
        }

        public IEnumerable<Facet> GetOutwardsFacets()
        {
            var facets = StlDocument.Facets;
            var center = facets.Select(_ => _.Center).Mean().ToVector3();
            var outwardsFacets =
                (from facet in facets
                    let ray = facet.Center.ToVector3() - center
                    where Vector3.Dot(ray, facet.Normal.ToVector3()) >= 0
                    select facet).ToList();
            return outwardsFacets;
        }

        #region [ Internal Methods ]

        private List<FacetsGroup> GroupByNormalStrict()
        {
            var bag = new Dictionary<Normal, HashSet<Facet>>();
            foreach (var facet in GetOutwardsFacets())
            {
                var normal = facet.Normal;
                if (!bag.ContainsKey(normal))
                    bag[normal] = new HashSet<Facet> {facet};
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
            var bag = new HashSet<Tuple<Vertex, HashSet<Facet>>>();
            foreach (var facet in GetOutwardsFacets())
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
                        bag.Add(new Tuple<Vertex, HashSet<Facet>>(
                            tFacets.Select(_ => _.Normal).Mean(),
                            tFacets));
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    bag.Add(new Tuple<Vertex, HashSet<Facet>>(facet.Normal, new HashSet<Facet> {facet}));
                }
            }
            return (from g in bag
                select new FacetsGroup
                {
                    Normal = NormalOffError(g.Item1),
                    Facets = g.Item2,
                }).ToList();
        }

        private Normal NormalOffError(Vertex v)
        {
            float x = v.X;
            float y = v.Y;
            float z = v.Z;
            float xa = Math.Abs(x);
            float ya = Math.Abs(y);
            float za = Math.Abs(z);
            if (xa > AxisAlignmentThreshold || ya > AxisAlignmentThreshold || za > AxisAlignmentThreshold)
            {
                if (xa < AxisAlignmentThreshold) x = 0;
                if (ya < AxisAlignmentThreshold) y = 0;
                if (za < AxisAlignmentThreshold) z = 0;
            }
            return new Normal(x, y, z);
        }

        #endregion

        public FacetsGroup FindCentralTube(FacetsGroup abutmentBase)
        {
            var facets = StlDocument.Facets.Except(abutmentBase.Facets);
            var tubeFacets = 
                (from facet in facets
                 let ray = new Vector3(facet.Center.X, facet.Center.Y, 0)
                 let normal = facet.Normal.ToVector3()
                 where Vector3.Dot(ray, normal) < 0
                 select facet);
            return new FacetsGroup {Facets = new HashSet<Facet>(tubeFacets)};
        }
    }
}
