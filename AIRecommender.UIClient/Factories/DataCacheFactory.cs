using AIRecommender.Cache;
using System;
using System.Configuration;
using System.Reflection;

namespace AIRecommender.UIClient
{
    public class DataCacheFactory
    {   
        public static DataCacheFactory Instance = new DataCacheFactory();
        protected DataCacheFactory()
        {

        }
        public virtual IDataCacher CreateDataCacher()
        {
            string className = ConfigurationManager.AppSettings["CACHE"];
            // Reflextion
            Type theType = Type.GetType(className);

            MethodInfo method = theType.GetMethod("Instance", BindingFlags.Static | BindingFlags.Public);

            return (IDataCacher)method.Invoke(null, null);
        }
    }
}
