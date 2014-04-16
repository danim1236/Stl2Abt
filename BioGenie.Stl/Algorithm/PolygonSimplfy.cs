using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public class PolygonSimplfy
    {
        private static List<int> _points;

        public static List<int> Points
        {
            get { return _points; }
        }

        public static List<Vertex> Simplify(List<Vertex> vertices, int numVertices, bool p3Maior)
        {
            _points = new List<int>
            {
                0, // P1
                vertices.Count - 1 // PLast
            };
            int maxPoints = numVertices - 2;

            var points = SimplifySection(vertices, 0, vertices.Count - 1, maxPoints);
            points.Sort();
            while (points.Count > maxPoints)
            {
                points.RemoveAt(0);
            }
            _points.AddRange(points.Select(_ => _.Item2));
            _points.Sort();
            var result = _points.Select(_ => new Vertex(vertices[_])).ToList();
            if (p3Maior)
            {
                var maxR = result[2].R;
                for (int i = 3; i < _points.Count; i++)
                {
                    var point = result[i];
                    if (point.R > maxR)
                        point.R = maxR;
                }
            }
            return result;
        }

        private static List<Tuple<double, int>> SimplifySection(List<Vertex> pts, int i, int j, int vertexToFind)
        {
            var points = new List<Tuple<double, int>>();
            if ((i + 1) == j)
                return new List<Tuple<double, int>>();

            var maxDistanceIndex = GetMaxDistanceIndex(pts, i, j);
            points.Add(maxDistanceIndex);
            vertexToFind--;
            if (vertexToFind > 0)
            {
                points.AddRange(SimplifySection(pts, i, maxDistanceIndex.Item2, vertexToFind));
                points.AddRange(SimplifySection(pts, maxDistanceIndex.Item2, j, vertexToFind));
            }
            return points;
        }

        private static Tuple<double, int> GetMaxDistanceIndex(List<Vertex> pts, int i, int j)
        {
            var a = pts[i].ToVector2();
            var b = pts[j].ToVector2();
            double maxDistance = -1.0;
            int maxIndex = i;
            for (int k = i + 1; k < j; k++)
            {
                double distance = DistancePointLine(pts[k].ToVector2(), a, b);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxIndex = k;
                }
            }
            return new Tuple<double, int>(maxDistance, maxIndex);
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
