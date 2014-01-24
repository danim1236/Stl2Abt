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

        private static List<Tuple<double, int>> _nPointsData; 
        public static List<int> Indexes { get; set; } 
        public static List<Vertex> FindNPoints(List<Vertex> vertices, int n)
        {
            var n2 = n - 2;
            _nPointsData = new List<Tuple<double, int>>();
            var lastPos = vertices.Count - 1;
            var llastPos = lastPos - 1;
            RecursiveFindPoints(vertices, 0, lastPos);
            _nPointsData.Sort();
            _nPointsData.Reverse();
            var removedBuffer = new HashSet<int>();
            bool removed;
            do
            {
                removed = false;
                for (int i = 0; i < n2; i++)
                {
                    var index1 = _nPointsData[i].Item2;
                    if (index1 == 1 || index1 == llastPos || 
                        removedBuffer.Contains(index1 + 1) ||
                        removedBuffer.Contains(index1 - 1))
                    {
                        removed = true;
                        _nPointsData.RemoveAt(i);
                        removedBuffer.Add(index1);
                        break;
                    }

                    for (int j = i + 1; j < n2; j++)
                    {
                        var index2 = _nPointsData[j].Item2;
                        if (index1 == index2 + 1 || index1 == index2 - 1)
                        {
                            removed = true;
                            _nPointsData.RemoveAt(i);
                            removedBuffer.Add(index1);
                            break;
                        }
                    }

                    if (removed)
                        break;
                }
            } while (removed);
            Indexes = new List<int> {0};
            Indexes.AddRange(_nPointsData.GetRange(0, n2).Select(_ => _.Item2));
            Indexes.Add(lastPos);
            Indexes.Sort();
            return Indexes.Select(_ => vertices[_]).ToList();
        }

        private static void RecursiveFindPoints(List<Vertex> pts, int i, int j)
        {
            if (i + 1 == j)
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
            _nPointsData.Add(new Tuple<double, int>(maxDistance, maxIndex));
            RecursiveFindPoints(pts, i, maxIndex);
            RecursiveFindPoints(pts, maxIndex, j);
        }

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
