using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BioGenie.Stl.Objects;

namespace BioGenie.Stl.Algorithm
{
    public class Abt
    {
        public Dictionary<float, List<Vertex>> Boundaries { get; set; }

        public Dictionary<float, List<Vertex>> GetPoints(int resVertical, bool p3Maior)
        {
            var result = new Dictionary<float, List<Vertex>>();
            var pairs = Boundaries.OrderBy(_=>_.Key).ToList();
            foreach (var pair in pairs)
            {
                var pontosNotaveis = GetPontosNotaveis2(pair.Value, resVertical, p3Maior);
                if (pontosNotaveis != null)
                    result[pair.Key] = pontosNotaveis;
            }
            return result;
        }

        private List<Vertex> GetPontosNotaveis2(List<Vertex> vertices, int resVertical, bool p3Maior)
        {
            var points = PolygonSimplfy.Simplify(vertices, resVertical, p3Maior ? GetMaxRIndex(vertices) : (int?) null);
            return points;
        }

        private int GetMaxRIndex(List<Vertex> vertices)
        {
            int index = 0;
            var maxR = float.MinValue;
            for (int i = 0; i < vertices.Count; i++)
            {
                var r = vertices[i].R;
                if (r > maxR)
                {
                    maxR = r;
                    index = i;
                }
            }
            return index;
        }

        public Abt(Dictionary<float, List<Vertex>> boundaries)
        {
            Boundaries = boundaries;
        }

        public static void WriteAbt(string fileName, Dictionary<float, List<Vertex>> abtBoundary)
        {

            var cultureInfo = CultureInfo.InvariantCulture;
            using (var file = File.Create(fileName))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
                    writer.WriteLine(
                        "<PartProject xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"conf/part_project.xsd\" FileExtension=\"abt\" ProjectType=\"Abutment\">");
                    writer.WriteLine("\t<Blank BaseDiameter=\"8.0\" TopDiameter=\"8.0\" Height=\"13.0\" />");
                    writer.WriteLine("\t<Faces>");

                    foreach (var pair in abtBoundary)
                    {
                        var theta = (pair.Key * 180.0 / Math.PI).ToString(cultureInfo);
                        var vertices = pair.Value;

                        var s1 = string.Format("\t\t<Face Name=\"\" AbsolutAngle=\"{0}\" EmergentPoint=\"-1\">", theta);
                        writer.WriteLine(s1);
                        writer.WriteLine("\t\t\t<Profile>");

                        foreach (var vertex in vertices)
                        {
                            var s = string.Format("\t\t\t\t<Point X=\"{0}\" Z=\"{1}\" />", vertex.Z.ToString(cultureInfo),
                                                  vertex.R.ToString(cultureInfo));
                            writer.WriteLine(s);
                        }

                        writer.WriteLine("\t\t\t</Profile>");
                        writer.WriteLine("\t\t</Face>");

                    }

                    writer.WriteLine("\t</Faces>");
                    writer.WriteLine("\t<AngularDisplacements X=\"0.0\" Y=\"0.0\" Z=\"0.0\" />");
                    writer.WriteLine("</PartProject>");
                }
            }
        }

    }
}