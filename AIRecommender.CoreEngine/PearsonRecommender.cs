using System;
using System.Collections.Generic;
using System.Linq;

namespace AIRecommender.CoreEngine
{
    public class PearsonRecommender : IRecommender
    {        
        public double GetCorrelation(int[] baseData, int[] otherData)
        {
            if(otherData.Length < baseData.Length)
            {
                otherData = otherData.Concat(Enumerable.Repeat(1, baseData.Length - otherData.Length)).ToArray();
            }
            else if(baseData.Length < otherData.Length)
            {
                otherData = otherData.Take(baseData.Length).ToArray();
            }

            if(baseData.Contains(0))
            {
                otherData = baseData.Select((x, i) => otherData[i] + (x == 0 ? 1 : 0)).ToArray();

                baseData = baseData.Select(x => x == 0 ? x + 1 : x).ToArray();
            }

            if(otherData.Contains(0))
            {
                baseData = otherData.Select((x, i) => baseData[i] + (x == 0 ? 1 : 0)).ToArray();

                otherData = otherData.Select(x => x == 0 ? x + 1 : x).ToArray();
            }

            double xSum = baseData.Sum();
            double ySum = otherData.Sum();

            int n = baseData.Length;

            double r1 = (n * baseData.Zip(otherData, (x, y) => x * y).Sum())-(xSum*ySum);

            double r2 = ((n * baseData.Sum(x => x * x)) - (xSum * xSum)) * ((n* otherData.Sum(x => x * x)) - (ySum*ySum));
            
            return r1/Math.Sqrt(r2);
        }
    }
}
