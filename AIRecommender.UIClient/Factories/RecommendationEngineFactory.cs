using AIRecommender.CoreEngine;
using System;
using System.Configuration;

namespace AIRecommender.UIClient
{
    public class RecommendationEngineFactory
    {
        protected RecommendationEngineFactory() { }

        public static readonly RecommendationEngineFactory Instance = new RecommendationEngineFactory();
        public virtual IRecommender CreateRecommendationEngine()
        {
            string className = ConfigurationManager.AppSettings["ALGO"];
            // Reflextion
            Type theType = Type.GetType(className);
            return (IRecommender)Activator.CreateInstance(theType);
        }
    }
}
