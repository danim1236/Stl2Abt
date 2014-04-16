using System;
using System.Collections.Generic;

namespace BioGenie.Stl.Tools
{
    public static class StatisticExtensions
    {
        public static float Mean(this List<float> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static float Mean(this List<float> values, int start, int end)
        {
            float s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
        }

        public static float Variance(this List<float> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }

        public static float Variance(this List<float> values, float mean)
        {
            return values.Variance(mean, 0, values.Count);
        }

        public static float Variance(this List<float> values, float mean, int start, int end)
        {
            float variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += (float)Math.Pow((values[i] - mean), 2);
            }

            int n = end - start;
            if (start > 0) n -= 1;

            return variance / (n);
        }

        public static float StdDev(this List<float> values)
        {
            return values.Count == 0 ? 0 : values.StdDev(0, values.Count);
        }

        public static float StdDev(this List<float> values, int start, int end)
        {
            float mean = values.Mean(start, end);
            float variance = values.Variance(mean, start, end);

            return (float) Math.Sqrt(variance);
        }
    }
}