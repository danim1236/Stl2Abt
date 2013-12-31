using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BioGenie.Stl.Tools;
using OpenTK;

namespace BioGenie.Stl.Objects
{
    public class Facet : IEquatable<Facet>
    {
        public List<Vertex> Vertices { get; set; }
        public int AttributeByteCount { get; set; }

        public Normal Normal
        {
            get { return _normal ?? (_normal = CalcNormal()); }
            set { _normal = value; }
        }

        public float Area
        {
            get { return (float) (_area ?? (_area = CalcArea())); }
        }

        public Vertex Center
        {
            get { return _center ?? (_center = CalcCenter()); }
        }

        public Facet()
        {
            Vertices = new List<Vertex>();
        }

        public Facet(Normal normal, IEnumerable<Vertex> vertices, int attributeByteCount)
            : this()
        {
            Normal = normal;
            Vertices = vertices.ToList();
            AttributeByteCount = attributeByteCount;
        }
        
        public bool Equals(Facet other)
        {
            return (Normal.Equals(other.Normal)
                    && Vertices.Count == other.Vertices.Count
                    && !Vertices.Where((o, i) => !o.Equals(other.Vertices[i])).Any());
        }

        public static Facet Read(StreamReader reader)
        {
            if (reader == null)
                return null;

            //Create the facet.
            var facet = new Facet();

            //Read the normal.
            if ((facet.Normal = Normal.Read(reader)) == null)
                return null;

            //Skip the "outer loop".
            reader.ReadLine();

            //Read 3 vertices.
            facet.Vertices = Enumerable.Range(0, 3).Select(o => Vertex.Read(reader)).ToList();

            //Read the "endloop" and "endfacet".
            reader.ReadLine();
            reader.ReadLine();

            return facet;
        }

        public static Facet Read(BinaryReader reader)
        {
            if (reader == null)
                return null;

            //Create the facet.
            var facet = new Facet
            {
                //Read the normal.
                Normal = Normal.Read(reader),
                //Read 3 vertices.
                Vertices = Enumerable.Range(0, 3).Select(o => Vertex.Read(reader)).ToList(),
                //Read the attribute byte count.
                AttributeByteCount = reader.ReadInt16()
            };
            return facet;
        }

        public void Write(StreamWriter writer)
        {
            writer.Write("\t");
            writer.WriteLine(ToString());
            writer.WriteLine("\t\touter loop");

            //Write each vertex.
            Vertices.ForEach(o => o.Write(writer));

            writer.WriteLine("\t\tendloop");
            writer.WriteLine("\tendfacet");
        }

        public void Write(BinaryWriter writer)
        {
            //Write the normal.
            Normal.Write(writer);

            //Write each vertex.
            Vertices.ForEach(o => o.Write(writer));
        }

        public override string ToString()
        {
            return "facet {0}".FormatString(Normal);
        }

        #region [ Dados Privados ]

        private float? _area;
        private Normal _normal;
        private Vertex _center;

        #endregion

        #region [ Calculos ]

        private Normal CalcNormal()
        {
            var vs = CalcAreaVector();
            return new Normal(vs.X/vs.Length, vs.Y/vs.Length, vs.Z/vs.Length);
        }

        private Vertex CalcCenter()
        {
            return Vertices.Mean();
        }
        
        private float CalcArea()
        {
            var vs = CalcAreaVector();
            return vs.Length/2;
        }

        private Vector3 CalcAreaVector()
        {
            var v1 = Vertices[0].ToVector3();
            var v2 = Vertices[1].ToVector3();
            var v3 = Vertices[2].ToVector3();
            var vs = Vector3.Cross(v1 - v2, v1 - v3);
            return vs;
        }

        #endregion
    }
}