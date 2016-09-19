using System;
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

        public static Vertex Mean(this IEnumerable<Facet> facets)
        {
            float x = 0;
            float y = 0;
            float z = 0;
            float totalArea = 0;
            var facetList = facets.ToList();
            foreach (var facet in facetList)
            {
                var area = facet.Area;
                var center = facet.Center;
                x += center.X * area;
                y += center.Y * area;
                z += center.Z * area;
                totalArea += area;
            }
            return new Vertex(x / totalArea, y / totalArea, z / totalArea);
        }

        public static float Area(this IEnumerable<Facet> facets)
        {
            return facets.Sum(_ => _.Area);
        }

        public static Tuple<float, float> AngularLimits(this Facet facet)
        {
            var results = new List<float>();

            var ts = facet.Vertices.Select(_ => _.ThetaZ).ToList();
            var t1 = ts.Min();
            var t2 = ts.Max();
            if (t1 >= Math.PI || t2 - t1 <= Math.PI)
            {
                results.Add(t1);
                results.Add(t2);
            }
            else
            {
                results.Add(t1);
                results.Add((float)-(2 * Math.PI - t2));
            }
            return new Tuple<float, float>(results.Min(), results.Max());
        }
    }
}