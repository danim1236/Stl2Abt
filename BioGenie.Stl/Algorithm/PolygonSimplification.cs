using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public class PolygonSimplification
    {
        private static float _distanceTolerance;
        private static bool[] _usePt;

        public static List<Vertex> DouglasPeuckerSimplify(List<Vertex> vertices, float tolerance)
        {
            _distanceTolerance = tolerance;
            _usePt = new bool[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
                _usePt[i] = true;

            SimplifySection(vertices, 0, vertices.Count - 1);

            return vertices.Where((t, i) => _usePt[i]).ToList();
        }

        public static List<int> UsedPoints
        {
            get
            {
                var result = new List<int>();
                for (int i = 0; i < _usePt.Length; i++)
                {
                    var b = _usePt[i];
                    if (b)
                        result.Add(i);
                }
                return result;
            }
        }

        private static void SimplifySection(List<Vertex> pts, int i, int j)
        {
            if ((i + 1) == j)
                return;

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
            if (maxDistance <= _distanceTolerance)
                for (int k = i + 1; k < j; k++)
                    _usePt[k] = false;
            else
            {
                SimplifySection(pts, i, maxIndex);
                SimplifySection(pts, maxIndex, j);
            }
        }
        
        private static double DistancePointPoint(Vector2 p, Vector2 p2)
        {
            double dx = p.X - p2.X;
            double dy = p.Y - p2.X;
            return Math.Sqrt(dx * dx + dy * dy);
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

            double r = ((p.X - a.X) * (b.X - a.X) + (p.Y - a.Y) * (b.Y - a.Y))
                        /
                        ((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));

            if (r <= 0.0) return DistancePointPoint(p, a);
            if (r >= 1.0) return DistancePointPoint(p, b);


            /*(2)
		                    (Ay-Cy)(Bx-Ax)-(Ax-Cx)(By-Ay)
		                s = -----------------------------
		             	                Curve^2

		                Then the distance from C to Point = |s|*Curve.
	        */

            double s = ((a.Y - p.Y) * (b.X - a.X) - (a.X - p.X) * (b.Y - a.Y))
                        /
                        ((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));

            return Math.Abs(s) * Math.Sqrt(((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y)));
        }
    }
}
