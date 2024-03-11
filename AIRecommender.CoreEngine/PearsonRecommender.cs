using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace AIRecommender.CoreEngine
{
    public class PearsonRecommender : IRecommender
    {
        /*public double GetCorrelation(int[] X, int[] Y)
        {
            if (Y.Length < X.Length)
            {
                Y = Y.Concat(Enumerable.Repeat(1, X.Length - Y.Length)).ToArray();
            }
            else if (X.Length < Y.Length)
            {
                Y = Y.Take(X.Length).ToArray();
            }

            if (X.Contains(0))
            {
                Y = X.Select((x, i) => Y[i] + (x == 0 ? 1 : 0)).ToArray();

                X = X.Select(x => x == 0 ? x + 1 : x).ToArray();
            }

            if (Y.Contains(0))
            {
                X = Y.Select((x, i) => X[i] + (x == 0 ? 1 : 0)).ToArray();

                Y = Y.Select(x => x == 0 ? x + 1 : x).ToArray();
            }

            int sum_X = 0, sum_Y = 0, sum_XY = 0;
            int squareSum_X = 0, squareSum_Y = 0;

            int n = X.Length;

            for (int i = 0; i < n; i++)
            {
                // sum of elements of array X.
                sum_X = sum_X + X[i];

                // sum of elements of array Y.
                sum_Y = sum_Y + Y[i];

                // sum of X[i] * Y[i].
                sum_XY = sum_XY + X[i] * Y[i];

                // sum of square of array elements.
                squareSum_X = squareSum_X + X[i] * X[i];
                squareSum_Y = squareSum_Y + Y[i] * Y[i];
            }

            // use formula for calculating correlation 
            // coefficient.
            float corr = (float)(n * sum_XY - sum_X * sum_Y) /
                         (float)(Math.Sqrt((n * squareSum_X -
                         sum_X * sum_X) * (n * squareSum_Y -
                         sum_Y * sum_Y)));

            return corr;
        }*/
        public double GetCorrelation(int[] baseData, int[] otherData)
        {
            if (otherData.Length < baseData.Length)
            {
                otherData = otherData.Concat(Enumerable.Repeat(1, baseData.Length - otherData.Length)).ToArray();
            }
            else if (baseData.Length < otherData.Length)
            {
                otherData = otherData.Take(baseData.Length).ToArray();
            }

            if (baseData.Contains(0))
            {
                otherData = baseData.Select((x, i) => otherData[i] + (x == 0 ? 1 : 0)).ToArray();

                baseData = baseData.Select(x => x == 0 ? x + 1 : x).ToArray();
            }

            if (otherData.Contains(0))
            {
                baseData = otherData.Select((x, i) => baseData[i] + (x == 0 ? 1 : 0)).ToArray();

                otherData = otherData.Select(x => x == 0 ? x + 1 : x).ToArray();
            }

            return Correlation.Pearson(baseData.Select(x => (double)x), otherData.Select(x=>(double)x));

            /*double xSum = baseData.Sum();
            double ySum = otherData.Sum();

            int n = baseData.Count();

            double numerator = (n * baseData.Zip(otherData, (x, y) => x * y).Sum()) - (xSum * ySum);
            double denominator = Math.Sqrt((n * baseData.Sum(x => x * x) - (xSum * xSum)) * (n * otherData.Sum(x => x * x) - (ySum * ySum)));

            if (denominator == 0)
            {
                return 0;
            }

            return numerator / denominator;*/
        }
    }
}
