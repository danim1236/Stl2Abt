using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Algorithm;
using OpenTK;

namespace BioGenie.Stl.Objects
{
    public sealed class StlAbutment : StlDocument
    {
        public float NormalTolThreshold = (float)0.0001;

        public StlAbutment(string name, IEnumerable<Facet> facets)
            : base(name, facets)
        {
        }

        public StlAbutment()
        {
        }

        public StlAbutment(StlDocument stlDocument)
        {
            Name = stlDocument.Name;
            Facets = stlDocument.Facets.Select(_ => new Facet(_.Normal, _.Vertices, _.AttributeByteCount)).ToList();
        }

        private FacetsGroup _abutmentBase;
        public double AngleThreshold = 0.005;
        private double? _maxZ;

        private FacetGrouper _facetGrouper;
        public FacetGrouper FacetGrouper {get { return _facetGrouper ?? (_facetGrouper = new FacetGrouper(this)); }}
        public FacetsGroup AbutmentBase
        {
            get { return _abutmentBase ?? (_abutmentBase = CalcGreaterSurface()); }
        }

        private FacetsGroup CalcGreaterSurface()
        {
            return FacetGrouper.GroupByNormal(NormalTolThreshold).OrderByDescending(_ => _.Area).FirstOrDefault();
        }

        public void AlignAndCenterAbutment()
        {
            AlignAbutment();
            CenterAbutment();
        }

        public void AlignAndCenterAbutment(AxisOrder axisOrder)
        {
            AlignAbutment(axisOrder);
            CenterAbutment();
        }

        public void AlignAbutment()
        {
            var normals = new[]
            {
                new Normal(0, 0, -1).ToVector3(),
                new Normal(0, 0, 1).ToVector3(),
                new Normal(-1, 0, 0).ToVector3(),
                new Normal(1, 0, 0).ToVector3(),
                new Normal(0, -1, 0).ToVector3(),
                new Normal(0, -1, 0).ToVector3()
            };
            var facetGroups = new[]
            {
                new List<Facet>(),
                new List<Facet>(),
                new List<Facet>(),
                new List<Facet>(),
                new List<Facet>(),
                new List<Facet>()
            };
            foreach (var facet in Facets)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Vector3.Dot(normals[i], facet.Normal.ToVector3()) > 0.999)
                    {
                        facetGroups[i].Add(facet);
                        break;
                    }
                }
            }
            var facetGroupAreas = facetGroups.Select((fs, i) => new Tuple<float, int>(fs.Sum(f => f.Area), i)).ToList();
            facetGroupAreas.Sort();
            var foundAxis = facetGroupAreas.Last().Item2;
            var axisOdermap = new[] {AxisOrder._Z, AxisOrder.Z, AxisOrder._X, AxisOrder.X, AxisOrder._Y, AxisOrder.Y};
            AlignAbutment(axisOdermap[foundAxis], facetGroups[foundAxis], facetGroupAreas.Last().Item1);
        }

        public double MaxZ
        {
            get { return (double) (_maxZ ?? (_maxZ = Facets.Max(_ => _.MaxZ))); }
        }

        private HashSet<Facet> _centralFacets;
        private HashSet<Facet> _shellFacets;

        public HashSet<Facet> CentralFacets
        {
            get { return _centralFacets ?? (_centralFacets = CalcCentralFacets().Facets); }
        }

        private FacetsGroup CalcCentralFacets()
        {
            return FacetGrouper.FindCentralTube(AbutmentBase);
        }

        private HashSet<Facet> _centralTubeFacets;
        public HashSet<Facet> CentralTubeFacets
        {
            get
            {
                return _centralTubeFacets ??
                       (_centralTubeFacets = new HashSet<Facet>(CentralFacets.Where(_ => _.Normal.Z < 0.1)));
            }
        }

        public void CenterAbutment()
        {
            var bag = new Dictionary<List<float>, List<Facet>>();
            var facets = AbutmentBase.Facets;
            var minZ = facets.Select(_ => _.MinZ).Min();
            var maxZ = facets.Select(_ => _.MaxZ).Max();
            var tolerance = (maxZ - minZ)/10;
            foreach (var facet in facets)
            {
                var found = false;
                var z = facet.Center.Z;
                foreach (var pair in bag)
                {
                    var zs = pair.Key;
                    var meanZ = zs.Sum()/zs.Count;
                    if (Math.Abs(meanZ - z) < tolerance)
                    {
                        zs.Add(z);
                        pair.Value.Add(facet);
                        found = true;
                        break;
                    }
                }
                if (!found)
                    bag.Add(new List<float> {z}, new List<Facet> {facet});
            }
            minZ = bag.Keys.Min(_ => _.Sum()/_.Count);
            var center = new Vertex(AbutmentBase.Center) {Z = minZ};
            foreach (var facet in Facets)
            {
                facet.Subtract(center);
                facet.Reset();
            }
            _centralFacets = null;
            _shellFacets = null;
        }

        public HashSet<Facet> ShellFacets
        {
            get
            {
                return _shellFacets ??
                       (_shellFacets = new HashSet<Facet>(Facets.Except(AbutmentBase.Facets).Except(CentralFacets).Where(_=>Math.Abs(_.Normal.Z) < 0.98)));
            }
        }

        public void AlignAbutment(AxisOrder axisOrder, List<Facet> baseFacets, float area)
        {
            switch (axisOrder)
            {
                case AxisOrder._Z:
                    break;
                case AxisOrder.Z:
                    foreach (var facet in Facets)
                    {
                        facet.Vertices = facet.Vertices.Select(_ => new Vertex(-_.X, _.Y, -_.Z)).ToList();
                        Normal normal = facet.Normal;
                        facet.Normal = new Normal(-normal.X, normal.Y, -normal.Z);
                        facet.Reset();
                    }
                    break;
                case AxisOrder._X:
                    foreach (var facet in Facets)
                    {
                        facet.Vertices = facet.Vertices.Select(_ => new Vertex(-_.Z, _.Y, _.X)).ToList();
                        Normal normal = facet.Normal;
                        facet.Normal = new Normal(-normal.Z, normal.Y, normal.X);
                        facet.Reset();
                    }
                    break;
                case AxisOrder.X:
                    foreach (var facet in Facets)
                    {
                        facet.Vertices = facet.Vertices.Select(_ => new Vertex(_.Z, _.Y, -_.X)).ToList();
                        Normal normal = facet.Normal;
                        facet.Normal = new Normal(normal.Z, normal.Y, -normal.X);
                        facet.Reset();
                    }
                    break;
                case AxisOrder._Y:
                    foreach (var facet in Facets)
                    {
                        facet.Vertices = facet.Vertices.Select(_ => new Vertex(_.X, -_.Z, _.Y)).ToList();
                        Normal normal = facet.Normal;
                        facet.Normal = new Normal(normal.X, -normal.Z, normal.Y);
                        facet.Reset();
                    }
                    break;
                case AxisOrder.Y:
                    foreach (var facet in Facets)
                    {
                        facet.Vertices = facet.Vertices.Select(_ => new Vertex(_.X, _.Z, -_.Y)).ToList();
                        Normal normal = facet.Normal;
                        facet.Normal = new Normal(normal.X, normal.Z, -normal.Y);
                        facet.Reset();
                    }
                    break;
            }
            _abutmentBase = new FacetsGroup
            {
                Facets = new HashSet<Facet>(baseFacets),
                Normal = new Normal(0, 0, -1),
                Area = area
            };
            _centralFacets = null;
            _shellFacets = null;
        }

        public void AlignAbutment(AxisOrder axisOrder)
        {
            var axisOdermap = new Dictionary<AxisOrder, Vector3>
            {
                {AxisOrder._Z, new Normal(0, 0, -1).ToVector3()},
                {AxisOrder.Z, new Normal(0, 0, 1).ToVector3()},
                {AxisOrder._X, new Normal(-1, 0, 0).ToVector3()},
                {AxisOrder.X, new Normal(1, 0, 0).ToVector3()},
                {AxisOrder._Y, new Normal(0, -1, 0).ToVector3()},
                {AxisOrder.Y, new Normal(0, -1, 0).ToVector3()}
            };

            var normal = axisOdermap[axisOrder];
            var baseFacets = Facets.Where(facet => Vector3.Dot(normal, facet.Normal.ToVector3()) > 0.999).ToList();
            AlignAbutment(axisOrder, baseFacets, baseFacets.Sum(_ => _.Area));
        }
    }
}