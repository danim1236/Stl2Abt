using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using BioGenie.Stl.Tools;

namespace BioGenie.Stl.Algorithm
{
    public class FacetsGroup
    {
        public HashSet<Facet> Facets { get; set; }
        public Normal Normal { get; set; }

        private float? _area;
        public float Area
        {
            get { return  (float) (_area ?? (_area = Facets.Sum(_ => _.Area))); }
            set { _area = value; }
        }

        private Vertex _center;
        public Vertex Center { get { return _center ?? (_center = Facets.Mean()); } }
    }
}