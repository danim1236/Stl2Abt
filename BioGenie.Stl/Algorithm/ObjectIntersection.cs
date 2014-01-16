using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl.Algorithm
{
    public static class ObjectIntersection
    {
        public const double EPSILON = 1E-6;


        public static Vertex Intersects(this Facet facet, Segment segment)
        {
            var p1 = segment.P1.ToVector3();
            var dir = segment.P2.ToVector3() - p1;
            var pa = facet.Vertices[0].ToVector3();
            var pb = facet.Vertices[1].ToVector3();
            var pc = facet.Vertices[2].ToVector3();

            // Calculate the parameters for the plane
            Vector3 n = Vector3.Cross(pb - pa, pc - pa);
            Normalize(ref n);
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
            Normalize(ref pa1);
            Normalize(ref pa2);
            Normalize(ref pa3);
            var a1 = Vector3.Dot(pa1, pa2);
            var a2 = Vector3.Dot(pa2, pa3);
            var a3 = Vector3.Dot(pa3, pa1);
            var total = Math.Acos(a1) + Math.Acos(a2) + Math.Acos(a3);
            if (Math.Abs(total - 2*Math.PI) < EPSILON) 
                return new Vertex(p);
            return null;
        }

        public static List<Vertex> Intersects(this Facet facet, Plane plane)
        {
            var segments = facet.GetEdgesAsSegments();
            if (1 - Vector3.Dot(facet.Normal.ToVector3(), plane.Normal.ToVector3()) < EPSILON)
                return null;
            var vertices = segments.Select(_ => _.Intersects(plane)).Where(__ => __ != null).ToList();
            return vertices;
        }

        public static Vertex Intersects(this Segment segment, Plane plane)
        {
            var p1 = segment.P1.ToVector3();
            var p2 = segment.P2.ToVector3();
            var dir = p2 - p1;
            var normal = plane.Normal.ToVector3();
            var vn = Vector3.Dot(dir, normal);
            if (Math.Abs(vn) < EPSILON)
                return null;
            var r = Vector3.Dot(normal, (plane.V0.ToVector3() - p1))/vn;
            if (r > 1 || r < -1)
                return null;
            return new Vertex(p1 + Vector3.Multiply(dir, r));
        }

        private static void Normalize(ref Vector3 n)
        {
            var l = n.Length;
            n.X /= l;
            n.Y /= l;
            n.Z /= l;
        }
    }
}
