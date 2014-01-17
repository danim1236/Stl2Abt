using System;
using System.Globalization;
using System.IO;
using BioGenie.Stl.Objects;
using System.Collections.Generic;

namespace BioGenie.Stl.Algorithm
{
    public static class AbtFile
    {
        public static void WriteAbt(this Dictionary<float, List<Vertex>> boundaryByTheta, string fileName)
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

                    foreach (var pair in boundaryByTheta)
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
