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
        public static List<Tuple<float, float>> AbutmentSize = new List<Tuple<float, float>>
        {
            new Tuple<float, float>(6, 11),
            new Tuple<float, float>(8, 13),
            new Tuple<float, float>(9, 11),
            new Tuple<float, float>(9.5F, 10),
            new Tuple<float, float>(9.5F, 11),
            new Tuple<float, float>(15, 11),
        };

        public Dictionary<float, List<Vertex>> Boundaries { get; set; }

        public Dictionary<float, List<Vertex>> GetPoints()
        {
            var result = new Dictionary<float, List<Vertex>>();
            var pairs = Boundaries.OrderBy(_=>_.Key).ToList();
            foreach (var pair in pairs)
            {
                var pontosNotaveis = GetPontosNotaveis(pair.Value);
                if (pontosNotaveis != null)
                    result[pair.Key] = pontosNotaveis;
            }
            return result;
        }

        private List<Vertex> GetPontosNotaveis(List<Vertex> vertices)
        {
            var points = PolygonSimplfy.Simplify(vertices);
            return points;
        }

        public Abt(Dictionary<float, List<Vertex>> boundaries)
        {
            Boundaries = boundaries;
        }

        public static void WriteAbt(string fileName, Dictionary<float, List<Vertex>> abtBoundary)
        {
            var maxZ = abtBoundary.Max(_ => _.Value.Max(__ => __.Z));
            var maxR = abtBoundary.Max(_ => _.Value.Max(__ => __.R));

            float? z=null, r=null;

            foreach (var tuple in AbutmentSize)
            {
                if (tuple.Item2 >= maxZ && tuple.Item1/2 >= maxR)
                {
                    z = tuple.Item2;
                    r = tuple.Item1;
                    break;
                }
            }
            if (!z.HasValue)
            {
                z = maxZ;
                r = maxR;
            }


            var cultureInfo = CultureInfo.InvariantCulture;
            using (var file = File.Create(fileName))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
                    writer.WriteLine(
                        "<PartProject xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"conf/part_project.xsd\" FileExtension=\"abt\" ProjectType=\"Abutment\">");
                    writer.WriteLine("\t<Blank BaseDiameter=\"{0}\" TopDiameter=\"{0}\" Height=\"{1}\" />", r.Value.ToString("N1", CultureInfo.InvariantCulture), z.Value.ToString("N1", CultureInfo.InvariantCulture));
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