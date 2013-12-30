using System.IO;
using BioGenie.Stl.Util;

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
            X = x;
            Y = y;
            Z = z;
        }

        public new static Normal Read(StreamReader reader)
        {
            return FromVertex(Vertex.Read(reader));
        }

        public new static Normal Read(BinaryReader reader)
        {
            return FromVertex(Vertex.Read(reader));
        }

        public static Normal FromVertex(Vertex vertex)
        {
            if (vertex == null)
                return null;

            return new Normal
            {
                X = vertex.X,
                Y = vertex.Y,
                Z = vertex.Z
            };
        }

        public override string ToString()
        {
            return "normal {0} {1} {2}".FormatString(X, Y, Z);
        }
    }
}