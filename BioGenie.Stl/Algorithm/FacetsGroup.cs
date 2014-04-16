using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using BioGenie.Stl.Tools;

namespace BioGenie.Stl.Algorithm
{
    public class FacetsGroup
    {
        public HashSet<Facet> Facets { get; set; }

        public Normal Normal
        {
            get { return _normal ?? (_normal = new Normal(Facets.Select(_=>_.Normal).Mean())); }
            set { _normal = value; }
        }

        private float? _area;
        public float Area
        {
            get { return  (float) (_area ?? (_area = Facets.Sum(_ => _.Area))); }
            set { _area = value; }
        }

        private Vertex _center;
        private Normal _normal;
        public Vertex Center { get { return _center ?? (_center = Facets.Mean()); } }

        public void Reset()
        {
            _center = null;
            _normal = null;
        }
    }
}