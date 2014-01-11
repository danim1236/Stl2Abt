using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Algorithm
{

    public class RevBoundary
    {
        public Dictionary<int, List<Vertex>> Boundaries { get; set; }

        public RevBoundary()
        {
            Boundaries = new Dictionary<int, List<Vertex>>();
        }

        public List<KeyValuePair<int, List<Vertex>>> GetOrderedBoundariesByRotStep()
        {
            return Boundaries.OrderBy(_ => _.Key).ToList();
        }
        public List<List<Vertex>> GetOrderedBoundaries()
        {
            return Boundaries.OrderBy(_ => _.Key).Select(_ => _.Value.OrderBy(__ => __.Z).ToList()).ToList();
        }

        public void WriteAbt(string fileName)
        {
            using (var file = File.Create(fileName))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
                    writer.WriteLine("<PartProject xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"conf/part_project.xsd\" FileExtension=\"abt\" ProjectType=\"Abutment\">");
                    writer.WriteLine("<Blank BaseDiameter=\"8.0\" TopDiameter=\"8.0\" Height=\"13.0\" />");
                    writer.WriteLine("<Faces>");

                    foreach (var vertices in Boundaries.Values)
                    {
                        var cultureInfo = CultureInfo.InvariantCulture;
                        var s1 = string.Format("<Face Name=\"\" AbsolutAngle=\"{0}\" EmergentPoint=\"-1\">", (vertices[0].Theta * 180.0 / Math.PI).ToString(cultureInfo));
                        writer.WriteLine(s1);
                        writer.WriteLine("<Profile>");

                        foreach (var vertex in vertices)
                        {
                            var s = string.Format("<Point X=\"{0}\" Z=\"{1}\" />", vertex.Z.ToString(cultureInfo), vertex.R.ToString(cultureInfo));
                            writer.WriteLine(s);
                        }

                        writer.WriteLine("</Profile>");
                        writer.WriteLine("</Face>");

                    }

                    writer.WriteLine("</Faces>");
                    writer.WriteLine("<AngularDisplacements X=\"0.0\" Y=\"0.0\" Z=\"0.0\" />");
                    writer.WriteLine("</PartProject>");
                }
            }
        }
    }
}