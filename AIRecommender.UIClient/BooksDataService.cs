using AIRecommender.Cache;
using AIRecommender.DataLoader;

namespace AIRecommender.UIClient
{
    public class BooksDataService
    {
        DataLoaderFactory dlFactory = DataLoaderFactory.Instance;
        IDataLoader _dataLoader = null;

        public BooksDataService() 
        { 
            _dataLoader = dlFactory.CreateDataLoader();
        }
        
        public BookDetails GetBookDetails()
        {
            IDataCacher cacher = MemDataCacher.Instance;

            if (cacher.GetData() == null)
            {
                cacher.SetData(_dataLoader.Load());
            }
            return cacher.GetData();
            
        }
    }
}
