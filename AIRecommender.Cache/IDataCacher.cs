using AIRecommender.DataLoader;

namespace AIRecommender.Cache
{
    public interface IDataCacher
    {
        BookDetails GetData();
        void SetData(BookDetails data);
    }
}
