using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Tools
{
    public static class StlTools
    {
        public static Vertex Mean(this IEnumerable<Vertex> vertices)
        {
            float x = 0;
            float y = 0;
            float z = 0;
            var verticeList = vertices.ToList();
            int count = verticeList.Count;
            foreach (var vertex in verticeList)
            {
                x += vertex.X;
                y += vertex.Y;
                z += vertex.Z;
            }
            return new Vertex(x/count, y/count, z/count);
        }
    }
}