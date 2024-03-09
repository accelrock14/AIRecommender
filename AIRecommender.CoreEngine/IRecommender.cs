namespace AIRecommender.CoreEngine
{
    public interface IRecommender
    {
        double GetCorrelation(int[] baseData, int[] otherData);
    }
}
