using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public class PolygonSimplfy
    {
        public static List<Vertex> Simplify(List<Vertex> vertices)
        {
            var p1 = vertices.First();
            var p6 = vertices.Last();
            int p3I = GetP3(vertices);
            var p3 = vertices[p3I];
            var p2I = GetParaDentro(vertices, 0, p3I);
            var p2 = vertices[p2I];
            var p5I = GetParaFora(vertices, p3I, vertices.Count - 1);
            var p5 = vertices[p5I];
            var p4I = GetParaDentro(vertices, p3I, p5I);
            var p4 = vertices[p4I];

            return new List<Vertex>
            {
                p1,
                p2,
                p3,
                p4,
                p5,
                p6
            };
        }
        
        private static int GetP3(IReadOnlyList<Vertex> vertices)
        {
            var maxR = double.MinValue;
            int offset = -1;
            for (int i = 0; i < vertices.Count; i++)
            {
                var vertex = vertices[i];
                if (vertex.R > maxR)
                {
                    maxR = vertex.R;
                    offset = i;
                }
            }
            return offset;
        }
        
        private static int GetParaDentro(IReadOnlyList<Vertex> vertices, int i, int j)
        {
            var a = vertices[i].ToVector2();
            var b = vertices[j].ToVector2();

            var ab = (b-a);

            var r = ab.Y/ab.X;

            var maxDistance = double.MinValue;
            var maxIndex = i;

            for (int index = i + 1; index < j; index++)
            {
                var vertex = vertices[index];
                var v = vertex.ToVector2();
                if ((v.X - a.X)*r > (v.Y - a.Y))
                {
                    var distance = DistancePointLine(v, a, b);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        maxIndex = index;
                    }
                }
            }

            return maxIndex;
        }

        private static int GetParaFora(IReadOnlyList<Vertex> vertices, int i, int j)
        {
            var a = vertices[i].ToVector2();
            var b = vertices[j].ToVector2();

            var ab = (b - a);

            var r = ab.Y / ab.X;

            var maxDistance = double.MinValue;
            var maxIndex = i;

            for (int index = i + 1; index < j; index++)
            {
                var vertex = vertices[index];
                var v = vertex.ToVector2();
                if ((v.X - a.X) * r < (v.Y - a.Y))
                {
                    var distance = DistancePointLine(v, a, b);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        maxIndex = index;
                    }
                }
            }

            return maxIndex;
        }

        private static double DistancePointPoint(Vector2 p, Vector2 p2)
        {
            double dx = p.X - p2.X;
            double dy = p.Y - p2.X;
            return Math.Sqrt(dx*dx + dy*dy);
        }

        private static double DistancePointLine(Vector2 p, Vector2 a, Vector2 b)
        {
            // if start == end, then use point-to-point distance
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (a.X == b.X && a.Y == b.Y)
                return DistancePointPoint(p, a);
            // ReSharper restore CompareOfFloatsByEqualityOperator

            // otherwise use comp.graphics.algorithms Frequently Asked Questions method
            /*(1)     	      AC dot AB
                        r =   ---------
                              ||AB||^2
             
		                r has the following meaning:
		                r=0 Point = A
		                r=1 Point = B
		                r<0 Point is on the backward extension of AB
		                r>1 Point is on the forward extension of AB
		                0<r<1 Point is interior to AB
	        */

            double r = ((p.X - a.X)*(b.X - a.X) + (p.Y - a.Y)*(b.Y - a.Y))
                       /
                       ((b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y));

            if (r <= 0.0) return DistancePointPoint(p, a);
            if (r >= 1.0) return DistancePointPoint(p, b);


            /*(2)
		                    (Ay-Cy)(Bx-Ax)-(Ax-Cx)(By-Ay)
		                s = -----------------------------
		             	                Curve^2

		                Then the distance from C to Point = |s|*Curve.
	        */

            double s = ((a.Y - p.Y)*(b.X - a.X) - (a.X - p.X)*(b.Y - a.Y))
                       /
                       ((b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y));

            return Math.Abs(s)*Math.Sqrt(((b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y)));
        }
    }
}
