using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BioGenie.Stl.Tools;

namespace BioGenie.Stl.Objects
{
    public class StlDocument : IEquatable<StlDocument>
    {
        public StlDocument()
        {
            Facets = new List<Facet>();
        }

        public StlDocument(string name, IEnumerable<Facet> facets)
            : this()
        {
            Name = name;
            Facets = facets.ToList();
        }

        public string Name { get; set; }

        public List<Facet> Facets { get; set; }

        public void GetLimits(out float xMin,out float yMin,out float zMin,out float xMax,out float yMax, out float zMax)
        {
            xMin = Facets.Min(_ => _.Vertices.Min(__ => __.X));
            yMin = Facets.Min(_ => _.Vertices.Min(__ => __.Y));
            zMin = Facets.Min(_ => _.Vertices.Min(__ => __.Z));
            xMax = Facets.Max(_ => _.Vertices.Max(__ => __.X));
            yMax = Facets.Max(_ => _.Vertices.Max(__ => __.Y));
            zMax = Facets.Max(_ => _.Vertices.Max(__ => __.Z));
        }

        public bool Equals(StlDocument other)
        {
            return (string.Equals(Name, other.Name)
                    && Facets.Count == other.Facets.Count
                    && !Facets.Where((o, i) => !o.Equals(other.Facets[i])).Any());
        }

        public static StlDocument Read(StreamReader reader)
        {
            const string regexSolid = @"solid\s+(?<Name>[^\r\n]+)?";

            if (reader == null)
                return null;

            //Read the header.
            string header = reader.ReadLine();
            Debug.Assert(header != null, "header != null");
            Match headerMatch = Regex.Match(header, regexSolid);
            Facet currentFacet;

            //Check the header.
            if (!headerMatch.Success)
                throw new FormatException(
                    "Invalid STL header, expected \"solid [name]\" but found \"{0}\".".FormatString(header));

            //Create the STL and extract the name (optional).
            var stl = new StlDocument
            {
                Name = headerMatch.Groups["Name"].Value
            };

            //Read each facet until the end of the stream.
            while ((currentFacet = Facet.Read(reader)) != null)
                stl.Facets.Add(currentFacet);

            return stl;
        }

        public static StlDocument Read(BinaryReader reader)
        {
            if (reader == null)
                return null;

            Facet currentFacet;

            //Read the header.
            byte[] buffer = reader.ReadBytes(80);
// ReSharper disable UnusedVariable
            string header = Encoding.ASCII.GetString(buffer);
// ReSharper restore UnusedVariable

            //Create the STL.
            var stl = new StlDocument();

            //Read (ignore) the number of triangles.
            reader.ReadBytes(4);

            //Read each facet until the end of the stream.
            while ((currentFacet = Facet.Read(reader)) != null)
            {
                stl.Facets.Add(currentFacet);

                if (reader.BaseStream.Position == reader.BaseStream.Length)
                    break;
            }

            return stl;
        }

        public void Write(StreamWriter writer)
        {
            //Write the header.
            writer.WriteLine(ToString());

            //Write each facet.
            Facets.ForEach(o => o.Write(writer));

            //Write the footer.
            writer.Write("end{0}".FormatString(ToString()));
        }

        public void Write(BinaryWriter writer)
        {
            //Write the header and facet count.
            writer.Write("Binary STL created by Quantum Concepts. www.quantumconceptscorp.com");
            writer.Write(Facets.Count);

            //Write each facet.
            Facets.ForEach(o => o.Write(writer));
        }

        public override string ToString()
        {
            return "solid {0}".FormatString(Name);
        }
    }
}