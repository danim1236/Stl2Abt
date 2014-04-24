using System.Collections.Generic;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Algorithm
{
    public class MeanNormalCalculator
    {
        public Normal Normal { get; private set; }
        public float TotalArea { get; private set; }
        public HashSet<Facet> Facets { get; private set; }

        public MeanNormalCalculator(Facet facet)
        {
            AddFacet(facet);
        }

        public void AddFacet(Facet facet)
        {
            var facetNormal = facet.Normal;
            var facetArea = facet.Area;
            if (Facets != null)
            {
                Facets.Add(facet);
                var newTotalArea = TotalArea + facetArea;
                Normal.X = ((Normal.X*TotalArea) + (facetNormal.X*facetArea)/newTotalArea);
                Normal.Y = ((Normal.Y*TotalArea) + (facetNormal.Y*facetArea)/newTotalArea);
                Normal.Z = ((Normal.Z*TotalArea) + (facetNormal.Z*facetArea)/newTotalArea);
                TotalArea = newTotalArea;
            }
            else
            {
                Facets = new HashSet<Facet> { facet };
                Normal = new Normal(facetNormal);
                TotalArea = facetArea;
            }
        }
    }
}