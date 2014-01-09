using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Algorithm
{

    public class RevBoundary
    {
        public Dictionary<int, List<Vertex>> Boundaries { get; set; }

        public RevBoundary()
        {
            Boundaries = new Dictionary<int, List<Vertex>>();
        }

        public List<KeyValuePair<int, List<Vertex>>> GetOrderedBoundariesByRotStep()
        {
            return Boundaries.OrderBy(_ => _.Key).ToList();
        }
        public List<List<Vertex>> GetOrderedBoundaries()
        {
            return Boundaries.OrderBy(_ => _.Key).Select(_ => _.Value.OrderBy(__ => __.Z).ToList()).ToList();
        }
    }
}