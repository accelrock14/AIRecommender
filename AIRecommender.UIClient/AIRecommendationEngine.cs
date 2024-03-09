using AIRecommender.CoreEngine;
using AIRecommender.DataAggrigator;
using AIRecommender.DataLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AIRecommender.UIClient
{
    public class AIRecommendationEngine
    {
        private readonly IDataLoader _dataLoader;
        private readonly IRatingsAggrigator _ratingsAggrigator;
        private readonly IRecommender _recommender;
        public AIRecommendationEngine(IDataLoader dataLoader, IRatingsAggrigator ratingsAggrigator, IRecommender recommender)
        {
            this._dataLoader = dataLoader;
            this._ratingsAggrigator = ratingsAggrigator;
            this._recommender = recommender;
        }
        public List<Book> Recommend(Preference preference, int limit)
        {
            Stopwatch sw = Stopwatch.StartNew();
            
            BookDetails bookDetails = _dataLoader.Load();
            Console.WriteLine("Loaded data: " + sw.ElapsedMilliseconds);
            
            Dictionary<string, List<int>> bookRatings = _ratingsAggrigator.Aggrigate(bookDetails, preference);
            Console.WriteLine("Aggrigated the ratings: " + sw.ElapsedMilliseconds);

            int[] baseData = bookDetails.ratings.Where(rating=>rating.ISBN == preference.ISBN).Select(rating=>rating.Rating).ToArray();

            List<Book> books = bookDetails.books.Where(book=> bookRatings.ContainsKey(book.ISBN)).ToList();

            books = books.OrderBy(book => _recommender.GetCorrelation(baseData, bookRatings[book.ISBN].ToArray())).ToList();
            Console.WriteLine("Obtained correlation: " + sw.ElapsedMilliseconds);
            sw.Stop();

            return books.Take(limit).ToList();
        }
    }
}
