using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using BioGenie.Stl.Util;
using OpenTK;

namespace BioGenie.Stl
{
    public class Vertex : IEquatable<Vertex>
    {
        public Vertex()
        {
        }

        public Vertex(float x, float y, float z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public bool Equals(Vertex other)
        {
            return (X.Equals(other.X)
                    && Y.Equals(other.Y)
                    && Z.Equals(other.Z));
        }

        public static Vertex Read(StreamReader reader)
        {
            const string regex = @"\s*(facet normal|vertex)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";

            float x, y, z;
            const NumberStyles numberStyle = (NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint |
                                              NumberStyles.AllowLeadingSign);

            if (reader == null)
                return null;

            string data = reader.ReadLine();

            if (data == null)
                return null;

            Match match = Regex.Match(data, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
                return null;

            if (!float.TryParse(match.Groups["X"].Value, numberStyle, CultureInfo.InvariantCulture, out x))
                throw new FormatException(
                    "Could not parse X coordinate \"{0}\" as a decimal.".FormatString(match.Groups["X"]));

            if (!float.TryParse(match.Groups["Y"].Value, numberStyle, CultureInfo.InvariantCulture, out y))
                throw new FormatException(
                    "Could not parse Y coordinate \"{0}\" as a decimal.".FormatString(match.Groups["Y"]));

            if (!float.TryParse(match.Groups["Z"].Value, numberStyle, CultureInfo.InvariantCulture, out z))
                throw new FormatException(
                    "Could not parse Z coordinate \"{0}\" as a decimal.".FormatString(match.Groups["Z"]));

            return new Vertex
            {
                X = x,
                Y = y,
                Z = z
            };
        }

        public static Vertex Read(BinaryReader reader)
        {
            if (reader == null)
                return null;

            var data = new byte[sizeof (float)*3];
            int bytesRead = reader.Read(data, 0, data.Length);

            //If no bytes are read then we're at the end of the stream.
            if (bytesRead == 0)
                return null;
            if (bytesRead != data.Length)
                throw new FormatException(
                    "Could not convert the binary data to a vertex. Expected 12 bytes but found {0}.".FormatString(
                        bytesRead));

            return new Vertex
            {
                X = BitConverter.ToSingle(data, 0),
                Y = BitConverter.ToSingle(data, 4),
                Z = BitConverter.ToSingle(data, 8)
            };
        }

        public void Write(StreamWriter writer)
        {
            writer.WriteLine("\t\t\t{0}".FormatString(ToString()));
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        public override string ToString()
        {
            return "vertex {0} {1} {2}".FormatString(X, Y, Z);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public Vector3 ToVector3(AxisOrder axisOrder)
        {
            switch (axisOrder)
            {
                case AxisOrder.X:
                    return new Vector3(X, Y, Z);
                case AxisOrder.Y:
                    return new Vector3(Y, Z, X);
                default:
                    return new Vector3(Z, X, Y);
            }
        }
    }
}