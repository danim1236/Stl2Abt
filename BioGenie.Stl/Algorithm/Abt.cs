using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BioGenie.Stl.Objects;
using OpenTK;

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
            var barrigaIndex = GetMaxRIndex(vertices);
            var tolerance = 2F;
            var points = PolygonSimplification.DouglasPeuckerSimplify(vertices, tolerance);
            while (points.Count < 6)
            {
                tolerance /= 2;
                points = PolygonSimplification.DouglasPeuckerSimplify(vertices, tolerance);
            }
            var indexes = PolygonSimplification.UsedPoints.ToArray().ToList();
            if (barrigaIndex != indexes[2])
            {
                if (Math.Abs(barrigaIndex - indexes[2]) > vertices.Count/10F)
                {
                    tolerance = 2F;
                    var verticesBelow = vertices.GetRange(0, barrigaIndex + 1);
                    points = PolygonSimplification.DouglasPeuckerSimplify(verticesBelow, tolerance);
                    while (points.Count < 3)
                    {
                        tolerance /= 2;
                        points = PolygonSimplification.DouglasPeuckerSimplify(verticesBelow, tolerance);
                    }
                    points = new List<Vertex> {points[0], points[1]};
                    indexes = PolygonSimplification.UsedPoints.ToArray().ToList();
                    indexes = new List<int> {indexes[0], indexes[1]};

                    tolerance = 2F;
                    var verticesAbove = vertices.GetRange(barrigaIndex, vertices.Count - barrigaIndex);
                    var pointsAbove = PolygonSimplification.DouglasPeuckerSimplify(verticesAbove, tolerance);
                    var indexesAbove = PolygonSimplification.UsedPoints.ToArray().ToList();
                    FilterAdjacentPoints(ref pointsAbove, ref indexesAbove);
                    
                    while (pointsAbove.Count < 4)
                    {
                        tolerance /= 2;
                        pointsAbove = PolygonSimplification.DouglasPeuckerSimplify(verticesAbove, tolerance);
                        indexesAbove = PolygonSimplification.UsedPoints.ToArray().ToList();
                        FilterAdjacentPoints(ref pointsAbove, ref indexesAbove);
                    }
                    points.AddRange(pointsAbove);
                    indexes.AddRange(indexesAbove.Select(_ => _ + barrigaIndex));
                }
            }

            while (points.Count > 6)
            {
                var coss = new List<Tuple<float, int>>();

                for (int i = 3; i < points.Count - 2; i++)
                {
                    var segment1 = points[i].ToVector2() - points[i - 1].ToVector2();
                    var segment2 = points[i + 1].ToVector2() - points[i].ToVector2();
                    coss.Add(new Tuple<float, int>(Vector2.Dot(segment1, segment2), i));
                }
                coss.Sort();
                points.RemoveAt(coss.First().Item2);

                //var newStart = indexes[2];
                //indexes = new List<int> {indexes[0], indexes[1]};
                //points = new List<Vertex> {points[0], points[1]};
                //tolerance = 2F;
                //var newVerts = vertices.GetRange(newStart, vertices.Count - newStart).ToList();
                //var points2 = PolygonSimplification.DouglasPeuckerSimplify(vertices, tolerance);
                //while (points2.Count < 4)
                //{
                //    tolerance /= 1.5F;
                //    points2 = PolygonSimplification.DouglasPeuckerSimplify(newVerts, tolerance);
                //}
                //var indexes2 = PolygonSimplification.UsedPoints.ToArray().ToList();
                //points.AddRange(points2);
                //indexes.AddRange(indexes2.Select(_ => _ + newStart));
                //while (points.Count > 6)
                //{
                //    var toRemove = points.Count - 2;
                //    points.RemoveAt(toRemove);
                //    indexes.RemoveAt(toRemove);
                //}
            }
            return points;
        }

        private void FilterAdjacentPoints(ref List<Vertex> pointsAbove, ref List<int> indexesAbove)
        {
            var groups = new List<List<int>>();
            var last = indexesAbove.Last();

            foreach (var i in indexesAbove)
            {
                var found = false;
                foreach (var @group in groups)
                {
                    if (@group.Any(i1 => i - i1 < 2))
                    {
                        @group.Add(i);
                        found = true;
                        break;
                    }
                }
                if (!found)
                    groups.Add(new List<int> {i});
            }
            var toRemove = groups.Where(_ => _.Count > 1).SelectMany(_ => _);
            foreach (var i in toRemove)
            {
                for (int j = 1; j < indexesAbove.Count - 1; j++)
                {
                    if (i == indexesAbove[j])
                    {
                        pointsAbove.RemoveAt(j);
                        indexesAbove.RemoveAt(j);
                        break;
                    }
                }
            }
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