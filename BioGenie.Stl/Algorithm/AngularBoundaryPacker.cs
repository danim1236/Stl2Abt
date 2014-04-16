using System;
using System.Collections.Generic;
using System.Linq;
using BioGenie.Stl.Objects;
using BioGenie.Stl.Tools;

namespace BioGenie.Stl.Algorithm
{
    public class AngularBoundaryPacker
    {
        public StlAbutment Abutment { get; set; }
        public int NRotSteps { get; set; }
        public float DeltaTheta { get; set; }
        public List<Facet> Facets { get; set; }

        public AngularBoundaryPacker(StlAbutment abutment, int nRotSteps)
        {
            Abutment = abutment;
            NRotSteps = nRotSteps;
            DeltaTheta = (float) (2*Math.PI/NRotSteps);
            Facets = abutment.ShellFacets.ToList();
        }

        public Dictionary<float, List<Vertex>> GetBoundaries(bool doFiltering)
        {
            var result = new Dictionary<float, List<Vertex>>();
            var fineBoundaries =
                new AngularBoundaryDetector(Abutment, NRotSteps * 10).GetBoundaries(doFiltering)
                    .OrderBy(_ => _.Key).ToList();
            var minTheta = -DeltaTheta / 2;
            for (int thetaI = 0; thetaI < NRotSteps; thetaI++)
            {
                var maxTheta = minTheta + DeltaTheta;
                var currentBoundaries = fineBoundaries.Where(_ => _.Key >= minTheta && _.Key <= maxTheta).ToList();

                var numVertices = new List<Tuple<int, int>>(currentBoundaries.Select(
                    (_, i) =>
                        new Tuple<int, int>(PolygonSimplification.DouglasPeuckerSimplify(_.Value, (float) 0.1).Count, i)));
                numVertices =
                    numVertices.OrderBy(_ => _.Item1).ThenBy(_ => Math.Abs(_.Item2-numVertices.Count/2)).ToList();

                var theta = thetaI * DeltaTheta;
                result.Add(theta, currentBoundaries[numVertices.First().Item2].Value.Select(
                    _ => Vertex.FromCilindric(_.R, theta, _.Z)).ToList());

                minTheta = maxTheta;
            }
            return result;
        }
    }
}