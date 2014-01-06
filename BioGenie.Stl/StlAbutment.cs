using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Objects;
using OpenTK;

namespace BioGenie.Stl
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

        public FacetsGroup AbutmentBase
        {
            get { return _abutmentBase ?? (_abutmentBase = CalcGreaterSurface()); }
        }

        private FacetsGroup CalcGreaterSurface()
        {
            return new FacetGrouper(this).GroupByNormal(NormalTolThreshold).OrderByDescending(_ => _.Area).FirstOrDefault();
        }

        public void CenterAbutment()
        {
        }

        public void AlignAndCenterAbutment()
        {
            var normal = AbutmentBase.Normal;
            
            double ty = 0;
            double tx = 0;
            if (Math.Abs(normal.X) > AngleThreshold)
            {
                ty = CalcQy(normal.Z, normal.X);
            }
            if (Math.Abs(normal.Y) > AngleThreshold)
            {
                tx = CalcQy(normal.Z, normal.Y);
            }

            var center = AbutmentBase.Center;
            foreach (var facet in Facets)
            {
                facet.Subtract(center);
                if (Math.Abs(ty) > AngleThreshold)
                {
                    facet.Rotate(Quaternion.FromAxisAngle(new Vector3(0, -1, 0), (float) ty));
                }
                if (Math.Abs(tx) > AngleThreshold)
                {
                    facet.Rotate(Quaternion.FromAxisAngle(new Vector3(1, 0, 0), (float) tx));
                }
            }
            _abutmentBase = null;
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
    }
}