using System;
using System.Collections.Generic;
using System.Linq;

namespace BioGenie.Stl.Algorithm
{

    public class RevBoundary
    {
        public Dictionary<int, AngularBoundary> Boundaries { get; set; }

        public RevBoundary()
        {
            Boundaries = new Dictionary<int, AngularBoundary>();
        }

        public List<KeyValuePair<int, AngularBoundary>> GetOrderedBoundaries()
        {
            return Boundaries.OrderBy(_ => _.Key).ToList();
        }
    }

    public class AngularBoundary
    {
        public List<Tuple<float, float>> RadiusAndZ { get; set; }

        public AngularBoundary()
        {
            RadiusAndZ = new List<Tuple<float, float>>();
        }
    }
}