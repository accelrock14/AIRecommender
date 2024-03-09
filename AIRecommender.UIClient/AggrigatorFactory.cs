using AIRecommender.DataAggrigator;
using System;
using System.Configuration;

namespace AIRecommender.UIClient
{
    public class AggrigatorFactory
    {
        protected AggrigatorFactory() { }

        public static readonly AggrigatorFactory Instance = new AggrigatorFactory();
        public virtual IRatingsAggrigator CreateAggrigator()
        {
            string className = ConfigurationManager.AppSettings["AGRI"];
            // Reflextion
            Type theType = Type.GetType(className);
            return (IRatingsAggrigator)Activator.CreateInstance(theType);
        }
    }
}
