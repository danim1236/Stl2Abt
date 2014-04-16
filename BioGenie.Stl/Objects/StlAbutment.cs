using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Tools;
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

        public FacetsGroup CalcGreaterSurface()
        {
            return FacetGrouper.GroupByNormal(NormalTolThreshold).OrderByDescending(_ => _.Area).FirstOrDefault();
        }

        public void AlignAndCenterAbutment()
        {
            if (AlignAbutmentOrtho(AbutmentBase))
            {
                CenterAbutment();
                return;
            }
            do
            {
                var normal = AbutmentBase.Normal;

                double ty = 0;
                double tx = 0;
                if (Math.Abs(normal.X) > AngleThreshold)
                {
                    ty = CalcQy(normal.Z, normal.X);
                }
                var quaternionY = Quaternion.FromAxisAngle(new Vector3(0, -1, 0), (float) ty);
                var newNormal = new Normal(normal);
                newNormal.Rotate(quaternionY);
                if (Math.Abs(newNormal.Y) > AngleThreshold)
                {
                    tx = CalcQy(newNormal.Z, newNormal.Y);
                }

                var center = AbutmentBase.Center;
                var quaternionX = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), (float) tx);
                foreach (var facet in Facets)
                {
                    facet.Subtract(center);
                    if (Math.Abs(ty) > AngleThreshold)
                    {
                        facet.Rotate(quaternionY);
                    }
                    if (Math.Abs(tx) > AngleThreshold)
                    {
                        facet.Rotate(quaternionX);
                    }
                }
                AbutmentBase.Reset();
            } while (AbutmentBase.Normal.Z > NormalTolThreshold - 1);
        }

        private static double CalcQy(float z, float x)
        {
            double t;
            if (z < 0)
            {
                if (x < 0)
                {
                    t = Math.Acos(-z);
                }
                else
                {
                    t = -Math.Acos(-z);
                }
            }
            else
            {
                if (x < 0)
                {
                    t = Math.PI - Math.Acos(z);
                }
                else
                {
                    t = Math.PI + Math.Acos(z);
                }
            }
            return t;
        }

        private bool AlignAbutmentOrtho(FacetsGroup abutmentBase)
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
            var axisOdermap = new[] {AxisOrder._Z, AxisOrder.Z, AxisOrder._X, AxisOrder.X, AxisOrder._Y, AxisOrder.Y};
            var normal = AbutmentBase.Normal.ToVector3();
            var th = 1 - NormalTolThreshold;
            var foundAxis = -1;
            for (int i = 0; i < normals.Length; i++)
            {
                if (Vector3.Dot(normal, normals[i]) > th)
                {
                    foundAxis = i;
                    break;
                }
            }
            var ret = foundAxis != -1;
            if (ret)
            {
                var abutmentBaseFacets = abutmentBase.Facets;
                AlignAbutmentOrtho(axisOdermap[foundAxis], abutmentBaseFacets.ToList(), abutmentBaseFacets.Area());
            }
            return ret;
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
            try
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
            catch
            {
            }
        }

        public HashSet<Facet> ShellFacets
        {
            get
            {
                return _shellFacets ??
                       (_shellFacets = new HashSet<Facet>(Facets.Except(AbutmentBase.Facets).Except(CentralFacets).Where(_=>Math.Abs(_.Normal.Z) < 0.98)));
            }
        }

        public void AlignAbutmentOrtho(AxisOrder axisOrder, List<Facet> baseFacets, float area)
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
    }
}