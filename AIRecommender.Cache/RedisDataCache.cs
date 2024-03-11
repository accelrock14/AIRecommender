using AIRecommender.DataLoader;
using System;
using StackExchange.Redis;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AIRecommender.Cache
{
    public class RedisDataCache : IDataCacher
    {
        readonly string key = "bookDetails";
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
            connection = ConnectionMultiplexer.Connect("localhost");

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
