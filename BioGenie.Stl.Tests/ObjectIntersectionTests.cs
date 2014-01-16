using System;
using System.Collections.Generic;
using System.Linq;
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
        #region FacetXSegment

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
                var vertices = new[]
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

        #endregion

        #region FacetXPlaneByOrigin

        [Test]
        public void FacetXPlaneByOriginSimple()
        {
            var facet = new Facet
            {
                Vertices = new List<Vertex>
                {
                    new Vertex(4, 3, 2),
                    new Vertex(-2, -5, -2),
                    new Vertex(6, -7, -2)
                }
            };
            var planeXY = new Plane {Normal = new Normal(0, 0, 1), V0 = new Vertex(0, 0, 0)};
            var intersections = facet.Intersects(planeXY);

            intersections.Count().Should().Be(2);
            intersections[0].X.Should().Be(1);
            intersections[0].Y.Should().Be(-1);
            intersections[0].Z.Should().Be(0);
            intersections[1].X.Should().Be(5);
            intersections[1].Y.Should().Be(-2);
            intersections[1].Z.Should().Be(0);
        }

        [Test]
        public void FacetXPlaneRandom()
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
                        new Vertex(4, 3, 2),
                        new Vertex(-2, -5, -2),
                        new Vertex(6, -7, -2)
                    }
                };
                var normal = new Normal(0, 0, 1);
                var p1 = new Vertex(1, -1, 0);
                var p2 = new Vertex(5, -2, 0);
                var vertices = new[]
                {
                    facet.Vertices[0],
                    facet.Vertices[1],
                    facet.Vertices[2],
                    normal,
                    p1,
                    p2
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

                var planeXY = new Plane {Normal = normal, V0 = new Vertex(0, 0, 0)};
                var intersections = facet.Intersects(planeXY);
                intersections.Count().Should().Be(2);
                Math.Round(intersections[0].X,3).Should().Be(Math.Round(p1.X, 3));
                Math.Round(intersections[0].Y, 3).Should().Be(Math.Round(p1.Y, 3));
                Math.Round(intersections[0].Z, 3).Should().Be(Math.Round(p1.Z, 3));
                Math.Round(intersections[1].X, 3).Should().Be(Math.Round(p2.X, 3));
                Math.Round(intersections[1].Y, 3).Should().Be(Math.Round(p2.Y, 3));
                Math.Round(intersections[1].Z, 3).Should().Be(Math.Round(p2.Z, 3));
            }
        }

        #endregion
    }
}
