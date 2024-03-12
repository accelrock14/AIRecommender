using AIRecommender.DataLoader;
using System.Data;
using System.Configuration;
using System.Xml.Linq;
using System;

namespace AIRecommender.Cache
{
    public class MemDataCacher : IDataCacher
    {
        public BookDetails bookDetails;
        private static MemDataCacher instance = null;

        public static IDataCacher Instance()
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
