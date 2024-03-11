using AIRecommender.DataLoader;

namespace AIRecommender.Cache
{
    public class MemDataCacher : IDataCacher
    {
        public BookDetails bookDetails;
        private static MemDataCacher instance = null;
        private static readonly object padlock = new object();

        public static MemDataCacher Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MemDataCacher();
                    }
                    return instance;
                }
            }
        }
        protected MemDataCacher()
        {
            bookDetails = null;
        }
        public BookDetails GetData()
        {
            return bookDetails;
        }

        public void SetData(BookDetails data)
        {
            bookDetails = data;
        }
    }
}
