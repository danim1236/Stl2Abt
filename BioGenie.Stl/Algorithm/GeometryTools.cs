using System;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public static class GeometryTools
    {
        public const double EPSILON = 1E-6;


        public static Vertex Intersects(this Facet facet, LineSegment lineSegment)
        {
            var p1 = lineSegment.P1.ToVector3();
            var dir = lineSegment.P2.ToVector3() - p1;
            var pa = facet.Vertices[0].ToVector3();
            var pb = facet.Vertices[1].ToVector3();
            var pc = facet.Vertices[2].ToVector3();

            // Calculate the parameters for the plane
            Vector3 n = Vector3.Cross(pb - pa, pc - pa);
            Normalise(ref n);
            float d = - Vector3.Dot(n, pa);

            // Calculate the position on the line that intersects the plane
            float denom = Vector3.Dot(n, dir);
            // Line and plane don't intersect
            if (Math.Abs(denom) < EPSILON)
                return null;
            float mu = - (d + Vector3.Dot(n, p1))/denom;
            Vector3 p = p1 + Vector3.Multiply(dir, mu);
            // Intersection not along line segment
            if (mu < 0 || mu > 1)
                return null;

            // Determine whether or not the intersection point is bounded by pa,pb,pc
            var pa1 = pa - p;
            var pa2 = pb - p;
            var pa3 = pc - p;
            Normalise(ref pa1);
            Normalise(ref pa2);
            Normalise(ref pa3);
            var a1 = Vector3.Dot(pa1, pa2);
            var a2 = Vector3.Dot(pa2, pa3);
            var a3 = Vector3.Dot(pa3, pa1);
            var total = Math.Acos(a1) + Math.Acos(a2) + Math.Acos(a3);
            if (Math.Abs(total - 2*Math.PI) < EPSILON) 
                return new Vertex(p);
            return null;
        }

        private static void Normalise(ref Vector3 n)
        {
            var l = n.Length;
            n.X /= l;
            n.Y /= l;
            n.Z /= l;
        }
    }
}
