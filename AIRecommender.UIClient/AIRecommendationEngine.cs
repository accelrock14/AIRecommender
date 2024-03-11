using AIRecommender.CoreEngine;
using AIRecommender.DataAggrigator;
using AIRecommender.DataLoader;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AIRecommender.UIClient
{
    /*public class AIRecommendationEngine
    {

        public IRatingsAggrigator ratingsAggrigator = null;
        public IRecommender recommender = null;
        public IDataLoader dataLoader = null;

        public AIRecommendationEngine(IRatingsAggrigator ratingsAggrigator, IRecommender recommender, IDataLoader dataLoader)
        {
            this.ratingsAggrigator = ratingsAggrigator;
            this.recommender = recommender;
            this.dataLoader = dataLoader;
        }

        public AIRecommendationEngine()
        {
            this.ratingsAggrigator = new RatingsAggrigator();
            this.recommender = new PearsonRecommender();
            this.dataLoader = new CSVDataLoader();
        }

        public List<Book> Recommend(Preference preference, int limit)
        {

            BookDetails bookDetails = dataLoader.Load();

            Dictionary<string, List<int>> aggrigatedRatingDict = ratingsAggrigator.Aggrigate(bookDetails, preference);

            int[] baseData = bookDetails.ratings.Where(rating => rating.ISBN == preference.ISBN).Select(rating => rating.Rating).ToArray();//aggrigatedRatingDict[preference.ISBN].ToArray();


            List<string> recommendedBooksISBN = aggrigatedRatingDict.OrderByDescending(record => recommender.GetCorrelation(baseData, record.Value.ToArray())).Select(record => record.Key).Take(limit).ToList();



            List<Book> finalBookList = new List<Book>();
            foreach (Book book in bookDetails.books)
            {
                if (recommendedBooksISBN.Contains(book.ISBN))
                {
                    finalBookList.Add(book);
                }
            }
            return finalBookList;
        }
    }*/
    public class AIRecommendationEngine
    {
        private readonly IRatingsAggrigator _ratingsAggrigator;
        private readonly IRecommender _recommender;

        public AIRecommendationEngine()
        {
            _ratingsAggrigator = new RatingsAggrigator();
            _recommender = new PearsonRecommender();
        }

        public AIRecommendationEngine(IRatingsAggrigator ratingsAggrigator, IRecommender recommender)
        {
            this._ratingsAggrigator = ratingsAggrigator;
            this._recommender = recommender;
        }
        public List<Book> Recommend(Preference preference, int limit)
        {
            BooksDataService booksDataService = new BooksDataService();

            Stopwatch sw = Stopwatch.StartNew();
            BookDetails bookDetails = booksDataService.GetBookDetails();
            Console.WriteLine("Loaded data: " + sw.ElapsedMilliseconds);
            
            sw = Stopwatch.StartNew();

            Dictionary<string, List<int>> bookRatings = _ratingsAggrigator.Aggrigate(bookDetails, preference);
            Console.WriteLine("Aggrigated the ratings: " + sw.ElapsedMilliseconds);
            sw = Stopwatch.StartNew();

            int[] baseData;
            if (bookRatings.ContainsKey(preference.ISBN))
                baseData = bookRatings[preference.ISBN].ToArray(); //bookDetails.ratings.Where(rating => rating.ISBN == preference.ISBN).Select(rating => rating.Rating).ToArray();
            else
            {
                // baseData = new int[0];
                throw new KeyNotFoundException("The Dictionary does not contain the ISBN present in the preference");
            }

            List<Book> books = bookDetails.books.Where(book => bookRatings.ContainsKey(book.ISBN)).ToList();
                        
            books = books.OrderByDescending(book => _recommender.GetCorrelation(baseData, bookRatings[book.ISBN].ToArray())).ToList();

            /*foreach (Book book in books.Take(limit))
            {
                Console.WriteLine("isbn: " + book.ISBN + " Correlation: " + _recommender.GetCorrelation(baseData, bookRatings[book.ISBN].ToArray())); //+ " built in: " + Correlation.Pearson(doubles,d2));
            }*/

            Console.WriteLine("Obtained correlation: " + sw.ElapsedMilliseconds);
            sw.Stop();

            return books.Take(limit).ToList();
        }
    }
}
