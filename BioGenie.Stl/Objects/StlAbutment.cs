using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Algorithm;

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
            Facets = stlDocument.Facets;
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

        private bool _alignAndCenterDone;
        public void AlignAndCenterAbutment()
        {
            if (!_alignAndCenterDone)
            {
                _alignAndCenterDone = true;
                CenterAbutment();
                //var normal = AbutmentBase.Normal;

                //double ty = 0;
                //double tx = 0;
                //if (Math.Abs(normal.X) > AngleThreshold)
                //{
                //    ty = CalcQy(normal.Z, normal.X);
                //}
                //if (Math.Abs(normal.Y) > AngleThreshold)
                //{
                //    tx = CalcQy(normal.Z, normal.Y);
                //}

                //var center = AbutmentBase.Center;
                //foreach (var facet in Facets)
                //{
                //    facet.Subtract(center);
                //    if (Math.Abs(ty) > AngleThreshold)
                //    {
                //        facet.Rotate(Quaternion.FromAxisAngle(new Vector3(0, -1, 0), (float) ty));
                //    }
                //    if (Math.Abs(tx) > AngleThreshold)
                //    {
                //        facet.Rotate(Quaternion.FromAxisAngle(new Vector3(1, 0, 0), (float) tx));
                //    }
                //}
                _abutmentBase = null;
                _centralFacets = null;
                _shellFacets = null;
            }
        }

        //private static double CalcQy(float z, float x)
        //{
        //    double t;
        //    if (z < 0)
        //    {
        //        if (x < 0)
        //        {
        //            t = Math.Acos(-z);
        //        }
        //        else
        //        {
        //            t = -Math.Acos(-z);
        //        }
        //    }
        //    else
        //    {
        //        if (x < 0)
        //        {
        //            t = Math.PI - Math.Acos(z);
        //        }
        //        else
        //        {
        //            t = Math.PI + Math.Acos(z);
        //        }
        //    }
        //    return t;
        //}

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
            AlignAndCenterAbutment();
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
            }
            _abutmentBase = null;
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
    }
}