using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Objects;
using BioGenie.Stl.Tests.Data;
using NUnit.Framework;
using SharpTestsEx;

namespace BioGenie.Stl.Tests
{
    [TestFixture]
    public class StlAlgorithmsTests
    {
        [Test]
        public void GroupFacetsByNormalError0Simple()
        {
            var document = new StlDocument
            {
                Facets = new List<Facet>
                {
                    new Facet
                    {
                        Normal = new Normal(1, 0, 0),
                        Vertices = new List<Vertex> {new Vertex(2, 0, 0), new Vertex(0, 3, 0), new Vertex(0, 0, 5)}
                    },
                    new Facet
                    {
                        Normal = new Normal(0, 1, 0),
                        Vertices = new List<Vertex> {new Vertex(1, 2, 3), new Vertex(5, 7, 11), new Vertex(13, 17, 19)}
                    }
                }
            };
            var groups = new FacetGrouper(document).GroupByNormal().OrderByDescending(_ => _.Area).ToList();
            groups.Count.Should().Be(2);
            Math.Round(groups[0].Area, 3).Should().Be(25.612);
            groups[0].Normal.Equals(new Normal(0, 1, 0)).Should().Be.True();
            groups[1].Area.Should().Be(9.5);
            groups[1].Normal.Equals(new Normal(1, 0, 0)).Should().Be.True();
        }

        [Test]
        public void GroupFacetsByNormalError1Simple()
        {
            var document = new StlDocument
            {
                Facets = new List<Facet>
                {
                    new Facet
                    {
                        Normal = new Normal(1, 0, 0),
                        Vertices = new List<Vertex> {new Vertex(2, 0, 0), new Vertex(0, 3, 0), new Vertex(0, 0, 5)}
                    },
                    new Facet
                    {
                        Normal = new Normal(0, 1, 0),
                        Vertices = new List<Vertex> {new Vertex(1, 2, 3), new Vertex(5, 7, 11), new Vertex(13, 17, 19)}
                    }
                }
            };
            var groups = new FacetGrouper(document).GroupByNormal(1).OrderByDescending(_ => _.Area).ToList();
            groups.Count.Should().Be(1);
            groups[0].Facets.Count.Should().Be(2);
            Math.Round(groups[0].Area, 3).Should().Be(35.112);
            Math.Round(groups[0].Normal.X, 5).Equals(Math.Round((float) (1/Math.Sqrt(2)), 5)).Should().Be.True();
            Math.Round(groups[0].Normal.Y, 5).Equals(Math.Round((float) (1/Math.Sqrt(2)), 5)).Should().Be.True();
            groups[0].Normal.Z.Should().Be(0);
        }

        [Test]
        public void StlAbutmentBase()
        {
            var document = new StlAbutment
            {
                Facets = new List<Facet>
                {
                    new Facet
                    {
                        Normal = new Normal(1, 0, 0),
                        Vertices = new List<Vertex> {new Vertex(2, 0, 0), new Vertex(0, 3, 0), new Vertex(0, 0, 5)}
                    },
                    new Facet
                    {
                        Normal = new Normal(0, 1, 0),
                        Vertices = new List<Vertex> {new Vertex(1, 2, 3), new Vertex(5, 7, 11), new Vertex(13, 17, 19)}
                    }
                }
            };
            var abutmentBase = document.AbutmentBase;
            Math.Round(abutmentBase.Area, 3).Should().Be(25.612);
            abutmentBase.Normal.Equals(new Normal(0, 1, 0)).Should().Be.True();
        }

        [Test]
        public void StlAbutmentBaseZ()
        {
            var document =
                new StlAbutment(
                    StlDocument.Read(
                        new BinaryReader(new MemoryStream(Resource.Dr_Juliano_SLM_Jonas_Jonas_single_CorB_abutment_11))));
            var abutmentBase = document.AbutmentBase;
            abutmentBase.Normal.X.Should().Be(0);
            abutmentBase.Normal.Y.Should().Be(0);
            abutmentBase.Normal.Z.Should().Be(-1);
        }

        [Test]
        public void StlAbutmentBaseX()
        {
            var document =
                new StlAbutment(
                    StlDocument.Read(
                        new BinaryReader(new MemoryStream(Resource.fabio))));
            var abutmentBase = document.AbutmentBase;
            abutmentBase.Normal.X.Should().Be(1);
            abutmentBase.Normal.Y.Should().Be(0);
            abutmentBase.Normal.Z.Should().Be(0);
        }

        [Test]
        public void StlAbutmentAlignAndCenterZ()
        {
            var document =
                new StlAbutment(
                    StlDocument.Read(
                        new BinaryReader(new MemoryStream(Resource.Dr_Juliano_SLM_Jonas_Jonas_single_CorB_abutment_11))));
            document.AlignAndCenterAbutment();
            var abutmentBase = document.AbutmentBase;
            Math.Round(abutmentBase.Center.X, 5).Should().Be(0);
            Math.Round(abutmentBase.Center.Y, 5).Should().Be(0);
            Math.Round(abutmentBase.Center.Z, 5).Should().Be(0);

            abutmentBase.Normal.X.Should().Be(0);
            abutmentBase.Normal.Y.Should().Be(0);
            abutmentBase.Normal.Z.Should().Be(-1);
        }

        [Test]
        public void StlAbutmentAlignAndCenterX()
        {
            var document =
                new StlAbutment(
                    StlDocument.Read(
                        new BinaryReader(new MemoryStream(Resource.fabio))));
            document.AlignAndCenterAbutment();
            var abutmentBase = document.AbutmentBase;
            Math.Round(abutmentBase.Center.X, 5).Should().Be(0);
            Math.Round(abutmentBase.Center.Y, 5).Should().Be(0);
            Math.Round(abutmentBase.Center.Z, 5).Should().Be(0);

            abutmentBase.Normal.X.Should().Be(0);
            abutmentBase.Normal.Y.Should().Be(0);
            abutmentBase.Normal.Z.Should().Be(-1);
        }
    }
}
