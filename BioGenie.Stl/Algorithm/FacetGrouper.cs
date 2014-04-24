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

        public List<FacetsGroup> GroupByNormal(float error)
        {
            var tol = 1 - error;
            var groups = new List<MeanNormalCalculator>();
            foreach (var facet in GetOutwardsFacets())
            {
                var normal = facet.Normal;
                bool found = false;
                foreach (var group in groups)
                {
                    if (Vector3.Dot(normal.ToVector3(), group.Normal.ToVector3()) >= tol)
                    {
                        group.AddFacet(facet);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    groups.Add(new MeanNormalCalculator(facet));
                }
            }
            return (from g in groups
                    select new FacetsGroup
                    {
                        Normal = NormalOffError(g.Normal),
                        Facets = g.Facets,
                    }).ToList();
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

        public FacetsGroup FindCentralTube(FacetsGroup abutmentBase)
        {
            var facets = StlDocument.Facets.Except(abutmentBase.Facets);
            var tubeFacets = (from facet in facets
                              let ray = new Vector3(facet.Center.X, facet.Center.Y, 0)
                              let normal = facet.Normal.ToVector3()
                              where Vector3.Dot(ray, normal) < 0
                              select facet).ToList();
            return new FacetsGroup {Facets = new HashSet<Facet>(tubeFacets)};
        }
    }
}
