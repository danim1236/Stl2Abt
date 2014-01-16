using System;
using System.Collections.Generic;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Objects;
using NUnit.Framework;
using OpenTK;
using SharpTestsEx;

namespace BioGenie.Stl.Tests
{
    [TestFixture]
    public class ObjectIntersectionTests
    {
        [Test]
        public void FacetXSegmentSimple()
        {
            var facet = new Facet
            {
                Vertices = new List<Vertex>
                {
                    new Vertex(0, 0, 0),
                    new Vertex(5, 0, 0),
                    new Vertex(0, 5, 0)
                }
            };
            var lineIn = new Segment(new Vertex(2.5F, 1, -1), new Vertex(2.5F, 1, 1));
            var lineOut = new Segment(new Vertex(2.5F, 4, -1), new Vertex(2.5F, 4, 1));
            facet.Intersects(lineIn).Equals(new Vertex(2.5F, 1, 0)).Should().Be.True();
            facet.Intersects(lineOut).Should().Be.Null();
        }

        [Test]
        public void FacetXSegmentRandom()
        {
            var r = new Random();
            for (int i = 0; i < 10; ++i)
            {
                var a = new Vector3(r.Next(), r.Next(), r.Next());
                var length = a.Length;
                a.X /= length;
                a.Y /= length;
                a.Z /= length;

                var quaternion = Quaternion.FromAxisAngle(a, (float) (r.NextDouble()*2*Math.PI));
                var facet = new Facet
                {
                    Vertices = new List<Vertex>
                    {
                        new Vertex(0, 0, 0),
                        new Vertex(5, 0, 0),
                        new Vertex(0, 5, 0)
                    }
                };
                var lineIn = new Segment(new Vertex(2.5F, 1, -1), new Vertex(2.5F, 1, 1));
                var lineOut = new Segment(new Vertex(2.5F, 4, -1), new Vertex(2.5F, 4, 1));
                var other = new Vertex(2.5F, 1, 0);
                var vertices = new []
                {
                    facet.Vertices[0],
                    facet.Vertices[1],
                    facet.Vertices[2],
                    lineIn.P1,
                    lineIn.P2,
                    lineOut.P1,
                    lineOut.P2,
                    other
                };
                foreach (var vertex in vertices)
                {
                    Vector3 vOut;
                    Vector3 n3 = vertex.ToVector3();
                    Vector3.Transform(ref n3, ref quaternion, out vOut);
                    vertex.X = vOut.X;
                    vertex.Y = vOut.Y;
                    vertex.Z = vOut.Z;
                    vertex.Reset();
                }
                var intersects = facet.Intersects(lineIn);
                Math.Round(intersects.X, 2).Should().Be(Math.Round(other.X, 2));
                Math.Round(intersects.Y, 2).Should().Be(Math.Round(other.Y, 2));
                Math.Round(intersects.Z, 2).Should().Be(Math.Round(other.Z, 2));
                facet.Intersects(lineOut).Should().Be.Null();
            }
        }
    }
}
