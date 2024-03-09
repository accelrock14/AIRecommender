using AIRecommender.DataAggrigator;
using AIRecommender.DataLoader;
using System;
using System.Collections.Generic;

namespace AIRecommender.UIClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataLoaderFactory dlFactory = DataLoaderFactory.Instance;
            RecommendationEngineFactory reFactory = RecommendationEngineFactory.Instance;
            AggrigatorFactory agFactory = AggrigatorFactory.Instance;

            AIRecommendationEngine aIRecommendationEngine = new AIRecommendationEngine(
                dlFactory.CreateDataLoader(), 
                agFactory.CreateAggrigator(), 
                reFactory.CreateRecommendationEngine());

            Preference p = new Preference
            {
                Age = 20,
                State = "california",
                ISBN = "0771074670"
            };
            List<Book> books = aIRecommendationEngine.Recommend(p, 10);

            foreach (Book book in books)
            {
                Console.WriteLine(book.BookTitle);
            }
        }
    }
}
