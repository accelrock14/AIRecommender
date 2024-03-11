using AIRecommender.DataLoader;
using System.Data;
using System;
using System.Configuration;

namespace AIRecommender.Cache
{
    public class MemDataCacher : IDataCacher
    {
        public BookDetails bookDetails;
        private static MemDataCacher instance = null;

        public static MemDataCacher Instance()
        {
            if (instance == null)
            {
                instance = new MemDataCacher();
            }
            return instance;
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
