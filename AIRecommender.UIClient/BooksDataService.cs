using AIRecommender.Cache;
using AIRecommender.DataLoader;

namespace AIRecommender.UIClient
{
    public class BooksDataService
    {
        readonly DataLoaderFactory dlFactory = DataLoaderFactory.Instance;
        readonly DataCacheFactory cacheFactory = DataCacheFactory.Instance;

        readonly IDataLoader _dataLoader = null;
        readonly IDataCacher _cacher = null;

        public BooksDataService() 
        { 
            _dataLoader = dlFactory.CreateDataLoader();
            _cacher = cacheFactory.CreateDataCacher();
        }
        
        public BookDetails GetBookDetails()
        {
            if (_cacher.GetData() == null)
            {
                _cacher.SetData(_dataLoader.Load());
            }
            return _cacher.GetData();
            
        }
    }
}
