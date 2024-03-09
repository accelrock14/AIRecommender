using AIRecommender.CoreEngine;
using AIRecommender.DataAggrigator;
using AIRecommender.DataLoader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Test
{
    internal class Program
    {
        public static void Main()
        {
            IRecommender pearsonRecommender = new PearsonRecommender();
            int[] a = { 1,2,3 };
            int[] b = { 2,4 };

            Stopwatch sw = Stopwatch.StartNew();

            CSVDataLoader cSVDataLoader = new CSVDataLoader();
            BookDetails bookDetails = cSVDataLoader.Load();

            Console.WriteLine(sw.ElapsedMilliseconds);
            /*foreach(BookUserRating book in bookDetails.ratings)
            {
                Console.WriteLine(book.ISBN + " " + book.UserID + " " + book.Rating);
            }*/

            Preference p = new Preference
            {
                Age = 20,
                State = "california",
                ISBN = "0771074670"
            };

            RatingsAggrigator ratingsAggrigator = new RatingsAggrigator();

            //ratingsAggrigator.Aggrigate(bookDetails, p);

            int[] arr = bookDetails.ratings.Where(s=>s.ISBN == p.ISBN).Select(n=>n.Rating).ToArray();

            Console.WriteLine(pearsonRecommender.GetCorrelation(a, b));
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Stop();
        }
    }
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
