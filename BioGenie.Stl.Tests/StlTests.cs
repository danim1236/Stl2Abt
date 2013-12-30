using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BioGenie.Stl.Tests.Data;
using NUnit.Framework;
using SharpTestsEx;

namespace BioGenie.Stl.Tests
{
    [TestFixture]
    public class StlTests
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
                new Facet(new Normal( 0, 0, 1), new List<Vertex>
                {
                    new Vertex( 0, 0, 0),
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

            stl1.Should().Equals(stl2);
            stl1String.Should().Equals(stl2String);
        }

        [Test]
        public void WriteBinary()
        {
            var stl1 = new StlDocument("WriteBinary", new List<Facet>
            {
                new Facet(new Normal( 0, 0, 1), new List<Vertex>
                {
                    new Vertex( 0, 0, 0),
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

            stl1.Should().Equals(stl2);
            stl1Data.Should().Equals(stl2Data);
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

            stls[0].Should().Equals(stls[1]);
        }
    }
}
