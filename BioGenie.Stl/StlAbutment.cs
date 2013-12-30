using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl
{
    public class StlAbutment : StlDocument
    {
        public StlAbutment(string name, IEnumerable<Facet> facets)
            : base(name, facets)
        {
        }

        public StlAbutment()
        {
        }

        public StlAbutment(StlDocument stlDocument)
        {
            Name = stlDocument.Name;
            Facets = stlDocument.Facets;
        }

        private FacetsGroup _greaterSurface;
        public FacetsGroup Base
        {
            get { return _greaterSurface ?? (_greaterSurface = CalcGreaterSurface()); }
        }

        private FacetsGroup CalcGreaterSurface()
        {
            return new FacetGrouper(this).GroupByNormal((float) 0.0001).OrderByDescending(_ => _.Area).FirstOrDefault();
        }
    }
}