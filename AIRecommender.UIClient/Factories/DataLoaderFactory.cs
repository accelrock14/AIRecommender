using AIRecommender.DataLoader;
using System;
using System.Configuration;

namespace AIRecommender.UIClient
{
    public class DataLoaderFactory
    {
        protected DataLoaderFactory() { }

        public static readonly DataLoaderFactory Instance = new DataLoaderFactory();
        public virtual IDataLoader CreateDataLoader()
        {
            string className = ConfigurationManager.AppSettings["LOAD"];
            // Reflextion
            Type theType = Type.GetType(className);
            return (IDataLoader)Activator.CreateInstance(theType);
        }
    }
}
