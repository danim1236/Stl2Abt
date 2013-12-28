using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BioGenie.Stl.Tests.Data;
using BioGenie.Stl.Util;
using NUnit.Framework;

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

            Assert.IsNotNull(stl);
            Assert.AreEqual(12, stl.Facets.Count);

            foreach (Facet facet in stl.Facets)
                Assert.AreEqual(3, facet.Vertices.Count);
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

            Assert.IsNotNull(stl);
            Assert.AreEqual(12, stl.Facets.Count);

            foreach (Facet facet in stl.Facets)
                Assert.AreEqual(3, facet.Vertices.Count);
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

            Assert.IsTrue(stl1.Equals(stl2));
            Assert.AreEqual(stl1String, stl2String);
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

            Assert.IsTrue(stl1.Equals(stl2));
            Assert.IsTrue(stl1Data.SequenceEqual(stl2Data));
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

            Assert.IsTrue(stls[0].Equals(stls[1]));
        }
    }
}
