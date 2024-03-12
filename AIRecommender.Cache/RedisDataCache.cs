using AIRecommender.DataLoader;
using System;
using StackExchange.Redis;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace AIRecommender.Cache
{
    public class RedisDataCache : IDataCacher
    {

        string key;
        string host;
        readonly ConnectionMultiplexer connection;
        readonly IDatabase cache;
        private static RedisDataCache instance = null;

        public static IDataCacher Instance()
        {
            if (instance == null)
            {
                instance = new RedisDataCache();
            }
            return instance;
        }
        protected RedisDataCache()
        {
            key = ConfigurationManager.AppSettings["KEY"];
            host = ConfigurationManager.AppSettings["HOST"];
            connection = ConnectionMultiplexer.Connect(host);

            cache = connection.GetDatabase();
        }
        public BookDetails GetData()
        {
            string serializedObject = cache.StringGet(key);
            if(serializedObject == RedisValue.EmptyString)
                return null;

            return JsonConvert.DeserializeObject<BookDetails>(serializedObject);
        }

        public void SetData(BookDetails data)
        {
            string serializedObject =  JsonConvert.SerializeObject(data);
                        
            cache.StringSet(key, serializedObject);
        }
    }
}
