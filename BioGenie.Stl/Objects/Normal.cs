using System;
using System.IO;
using BioGenie.Stl.Tools;
using OpenTK;

namespace BioGenie.Stl.Objects
{
    public class Normal : Vertex
    {
        public Normal()
        {
        }

        public Normal(float x, float y, float z)
            : this()
        {
            // if (x != x) :: Teste classico para NaN
            if ( ( x != x ) || ( y != y ) || ( z != z ) )
            {
                throw new Exception("Cannot create a normal with NaN values");
            }
            X = x;
            Y = y;
            Z = z;
            NormalizeLength();
        }

        public Normal(Vertex v)
            : this(v.X, v.Y, v.Z)
        {
        }

        public Normal(Vector3 v)
            : this(v.X, v.Y, v.Z)
        {
        }

        public new static Normal Read(StreamReader reader)
        {
            var vertex = Vertex.Read(reader);
            return vertex != null ? new Normal(vertex) : null;
        }

        public new static Normal Read(BinaryReader reader)
        {
            var vertex = Vertex.Read(reader);
            return vertex != null ? new Normal(vertex) : null;
        }

        public override string ToString()
        {
            return "normal {0} {1} {2}".FormatString(X, Y, Z);
        }

        private void NormalizeLength()
        {
            var l = ToVector3().Length;
            if (l == 0)
            {
                throw new Exception("Cannot normalize a normal with length 0");
            }
            X /= l;
            Y /= l;
            Z /= l;
            Reset();
        }
    }
}