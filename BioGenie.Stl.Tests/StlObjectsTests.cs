using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BioGenie.Stl.Objects;
using BioGenie.Stl.Tests.Data;
using NUnit.Framework;
using OpenTK;
using SharpTestsEx;

namespace BioGenie.Stl.Tests
{
    [TestFixture]
    public class StlObjectsTests
    {
        [Test]
        public void FromString()
        {
            StlDocument stl;

            using (Stream stream = new MemoryStream(Resource.ASCII))
            {
                using (var reader = new StreamReader(stream))
                {
                    stl = StlDocument.Read(reader);
                }
            }

            stl.Should().Not.Be.Null();
            stl.Facets.Count.Should().Be(12);
            foreach (Facet facet in stl.Facets)
                facet.Vertices.Count.Should().Be(3);
        }

        [Test]
        public void FromBinary()
        {
            StlDocument stl;

            using (Stream stream = new MemoryStream(Resource.Binary))
            {
                using (var reader = new BinaryReader(stream))
                {
                    {
                        stl = StlDocument.Read(reader);
                    }
                }
            }

            stl.Should().Not.Be.Null();
            stl.Facets.Count.Should().Be(12);
            foreach (Facet facet in stl.Facets)
                facet.Vertices.Count.Should().Be(3);
        }

        [Test]
        public void WriteString()
        {
            var stl1 = new StlDocument("WriteString", new List<Facet>
            {
                new Facet(new Normal(0, 0, 1), new List<Vertex>
                {
                    new Vertex(0, 0, 0),
                    new Vertex(-10, -10, 0),
                    new Vertex(-10, 0, 0)
                }, 0)
            });
            StlDocument stl2;
            byte[] stl1Data;
            string stl1String;
            string stl2String;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    stl1.Write(writer);
                }

                stl1Data = stream.ToArray();
                stl1String = Encoding.ASCII.GetString(stl1Data);
            }

            using (var stream = new MemoryStream(stl1Data))
            {
                using (var reader = new StreamReader(stream))
                {
                    stl2 = StlDocument.Read(reader);
                }

                byte[] stl2Data = stream.ToArray();
                stl2String = Encoding.ASCII.GetString(stl2Data);
            }

            stl1.Equals(stl2).Should().Be.True();
            stl1String.Equals(stl2String).Should().Be.True();
        }

        [Test]
        public void WriteBinary()
        {
            var stl1 = new StlDocument("WriteBinary", new List<Facet>
            {
                new Facet(new Normal(0, 0, 1), new List<Vertex>
                {
                    new Vertex(0, 0, 0),
                    new Vertex(-10, -10, 0),
                    new Vertex(-10, 0, 0)
                }, 0)
            });
            StlDocument stl2;
            byte[] stl1Data;
            byte[] stl2Data;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    stl1.Write(writer);
                }

                stl1Data = stream.ToArray();
            }

            using (var stream = new MemoryStream(stl1Data))
            {
                using (var reader = new StreamReader(stream))
                {
                    stl2 = StlDocument.Read(reader);
                }

                stl2Data = stream.ToArray();
            }

            stl1.Equals(stl2).Should().Be.True();
            stl1Data.SequenceEqual(stl2Data).Should().Be.True();
        }

        [Test]
        public void Equality()
        {
            var stls = new StlDocument[2];

            for (int i = 0; i < stls.Length; i++)
            {
                using (Stream stream = new MemoryStream(Resource.ASCII))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        stls[i] = StlDocument.Read(reader);
                    }
                }
            }

            stls[0].Equals(stls[1]).Should().Be.True();
        }

        [Test]
        public void FacetAreaSimple()
        {
            var facet = new Facet
            {
                Vertices = new List<Vertex>
                {
                    new Vertex(2, 0, 0),
                    new Vertex(0, 3, 0),
                    new Vertex(0, 0, 5),
                }
            };
            facet.Area.Should().Be(9.5);
        }

        [Test]
        public void FacetAreaPrimos()
        {
            var facet = new Facet
            {
                Vertices = new List<Vertex>
                {
                    new Vertex(1, 2, 3),
                    new Vertex(5, 7, 11),
                    new Vertex(13, 17, 19),
                }
            };
            Math.Round(facet.Area, 3).Should().Be(25.612);
        }

        [Test]
        public void LineSegmentLength()
        {
            var line = new Segment(new Vertex(1, 2, 3), new Vertex(4, 6, 8));
            Math.Round(Math.Pow(line.Length, 2), 5).Should().Be(50);
        }

        [Test]
        public void V1CrossV2()
        {
            var r = new Random();
            var pa = new Vector3(r.Next(), r.Next(), r.Next());
            var pb = new Vector3(r.Next(), r.Next(), r.Next());
            var pc = new Vector3(r.Next(), r.Next(), r.Next());
            var nLong = new Vector3();
            nLong.X = (pb.Y - pa.Y) * (pc.Z - pa.Z) - (pb.Z - pa.Z) * (pc.Y - pa.Y);
            nLong.Y = (pb.Z - pa.Z) * (pc.X - pa.X) - (pb.X - pa.X) * (pc.Z - pa.Z);
            nLong.Z = (pb.X - pa.X) * (pc.Y - pa.Y) - (pb.Y - pa.Y) * (pc.X - pa.X);

            var nShort = Vector3.Cross(pb - pa, pc - pa);

            nShort.Equals(nLong).Should().Be.True();
        }

    }
}
