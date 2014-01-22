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
            var pairs = Boundaries.OrderBy(_=>_.Key).ToList();
            var a = Get6PontosNotaveis(pairs.Last().Value);
            foreach (var pair in pairs)
            {
                result[pair.Key] = Get6PontosNotaveis(pair.Value);
            }
            return result;
        }

        private List<Vertex> Get6PontosNotaveis(List<Vertex> vertices)
        {
            var result = new Vertex[6];
            var barrigaIndex = GetMaxRIndex(vertices);

            result[0] = vertices.First();
            result[2] = vertices[barrigaIndex];
            result[5] = vertices.Last();
            var pairs = vertices.Select(_ => new Tuple<float, float>(_.Z, _.R)).ToList();
            var dd1 = CalcDerivada(pairs).ToList();
            var inflexao = FindInflexao(dd1);
            var inflexaoBelow = inflexao.FirstOrDefault(_ => _.Item2 < barrigaIndex) ??
                                new Tuple<float, int, float>(0, barrigaIndex/2, 0);
            var inflexoesAbove = inflexao.Where(_ => _.Item2 > barrigaIndex).ToList().GetRange(0, 2).OrderBy(_ => _.Item2).ToList();
            result[1] = vertices[inflexaoBelow.Item2];
            result[3] = vertices[inflexoesAbove[0].Item2];
            result[4] = vertices[inflexoesAbove[1].Item2];
            return result.ToList();
        }

        protected List<Tuple<float, int, float>> FindInflexao(IEnumerable<Tuple<float, float>> a)
        {
            var d2S = a.Select(_ => _.Item2).ToList();
            var result = new List<Tuple<float, int, float>>();
            var result2 = new List<Tuple<float, int, float>>();
            
            for (int i = 1; i < d2S.Count; i++)
            {
                result.Add(new Tuple<float, int, float>(Math.Abs((d2S[i] - d2S[i - 1])/((d2S[i] + d2S[i - 1])/2)), i, d2S[i]));
            }
            result.Sort();
            result.Reverse();
            for (int i = 0; i < result.Count; i++)
            {
                var jaTem = false;
                var tuple = result[i];
                for (int j = 0; j < i; j++)
                {
                    var tuple1 = result[j];
                    var diff = Math.Abs(tuple1.Item2 - tuple.Item2);
                    if (diff < 3)
                    {
                        jaTem = true;
                        break;
                    }
                }
                if (!jaTem)
                    result2.Add(tuple);
            }

            return result2;
        }

        private IEnumerable<Tuple<float, float>> CalcDerivada(List<Tuple<float, float>> pairs)
        {
            var result = new List<Tuple<float, float>>();
            for (int i = 1; i < pairs.Count; i++)
            {
                result.Add(new Tuple<float, float>(pairs[i].Item1,
                                                   (pairs[i].Item2 - pairs[i - 1].Item2)/
                                                   (pairs[i].Item1 - pairs[i - 1].Item1)));
            }
            return result;
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