using AIRecommender.DataAggrigator;
using AIRecommender.DataLoader;
using System;
using System.Collections.Generic;
using System.IO;

namespace AIRecommender.UIClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RecommendationEngineFactory reFactory = RecommendationEngineFactory.Instance;
            AggrigatorFactory agFactory = AggrigatorFactory.Instance;

            AIRecommendationEngine aIRecommendationEngine = new AIRecommendationEngine(
                agFactory.CreateAggrigator(),
                reFactory.CreateRecommendationEngine());

            List<Book> books;
            /*Preference p = new Preference
            {
                Age = 40,
                State = "california",
                ISBN = "0425182908"
            };*/

            Preference p = new Preference();
            while(true)
            {                
                try
                {
                    Console.WriteLine("Enter User age: ");
                    p.Age = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter User state: ");
                    p.State = Console.ReadLine();
                    Console.WriteLine("Enter Book ISBN: ");
                    p.ISBN = Console.ReadLine();
                    Console.WriteLine();
                    books = aIRecommendationEngine.Recommend(p, 10);
                    Console.WriteLine();
                    foreach (Book book in books)
                    {
                        Console.WriteLine($"ISBN-{book.ISBN}\tTITLE: {book.BookTitle}");
                    }
                    Console.WriteLine();
                }
                catch(FormatException ex)
                {
                    Console.WriteLine("Age should be an integer value");
                }
                catch (KeyNotFoundException ex)
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
}
