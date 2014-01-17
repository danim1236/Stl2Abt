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
        private Dictionary<float, List<Vertex>> _abtBoundaries;
        public Dictionary<float, List<Vertex>> Boundaries { get; set; }

        public Dictionary<float, List<Vertex>> AbtBoundaries
        {
            get { return _abtBoundaries ?? (_abtBoundaries = Get6Points()); }
        }

        public Dictionary<float, List<Vertex>> Get6Points()
        {
            var result = new Dictionary<float, List<Vertex>>();
            foreach (var pair in Boundaries)
            {
                result[pair.Key] = Get6PontosNotaveis(pair.Value);
            }
            return result;
        }

        private List<Vertex> Get6PontosNotaveis(List<Vertex> vertices)
        {
            var result = new Vertex[5];
            result[0] = vertices.First();
            result[4] = vertices.Last();
            var barrigaIndex = GetMaxRIndex(vertices);
            result[2] = vertices[barrigaIndex];
            result[1] = vertices[barrigaIndex/2];
            var primeiraInflexao = GetPrimeiraInflexaoParaFora(vertices.GetRange(barrigaIndex, vertices.Count-barrigaIndex)) + barrigaIndex;
            if (primeiraInflexao >= 0)
                result[3] = vertices[primeiraInflexao];
            else 
                result[3] = vertices[(vertices.Count + primeiraInflexao)/2];
            return result.ToList();
        }

        private int GetPrimeiraInflexaoParaFora(List<Vertex> vertices)
        {
            var derivadas = new List<float>();
            for (int i = 1; i < vertices.Count; i++)
            {
                derivadas.Add(vertices[i].R - vertices[i - 1].R);
            }
            var derivadas2Indexadas = new List<Tuple<float, int>>();
            for (int i = 1; i < derivadas.Count; i++)
            {
                var diff = derivadas[i] - derivadas[i - 1];
                derivadas2Indexadas.Add(new Tuple<float, int>(diff, i + 1));
            }
            if (derivadas2Indexadas.Any())
            {
                derivadas2Indexadas.Sort();
                return derivadas2Indexadas[derivadas2Indexadas.Count-2].Item2;
            }
            return -1;
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

        public void WriteAbt(string fileName)
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

                    foreach (var pair in Get6Points())
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