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

        private FacetsGroup _greaterSurface;
        public FacetsGroup GreaterSurface
        {
            get { return _greaterSurface ?? (_greaterSurface = CalcGreaterSurface()); }
        }

        private FacetsGroup CalcGreaterSurface()
        {
            return new FacetGrouper(this).GroupByNormal(0).OrderByDescending(_ => _.Area).FirstOrDefault();
        }
    }
}