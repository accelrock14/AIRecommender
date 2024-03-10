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

            List<Book> books;

            /*Preference p = new Preference
            {
                Age = 30,
                State = "arizona",
                ISBN = "0446310786"
            };*/

            Preference p = new Preference
            {
                Age = 40,
                State = "california",
                ISBN = "0425182908"
            };

            try
            {
                books = aIRecommendationEngine.Recommend(p, 10);
                foreach (Book book in books)
                {
                    Console.WriteLine(book.BookTitle);
                }
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }            
        }
    }
}
