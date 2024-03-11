using AIRecommender.DataLoader;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIRecommender.DataAggrigator
{
    public class RatingsAggrigator : IRatingsAggrigator
    {
        public enum AgeGroup { TeenAge=16, YoungAge=30, MidAge=50, OldAge=60, SeniorCitizens=100, NULL=0 }
        public Dictionary<string, List<int>> Aggrigate(BookDetails bookDetails, Preference preference)
        {
            Dictionary<string, List<int>> bookRatings = new Dictionary<string, List<int>>();

            List<int> users = bookDetails.users.Where(user => user.State == preference.State && GetAgeGroup(user.Age) == GetAgeGroup(preference.Age)).Select(user => user.UserID).ToList();
            //List<int> uid = users.Select(user => user.UserID).ToList();

            ConcurrentBag<BookUserRating> ratings = new ConcurrentBag<BookUserRating>();// = bookDetails.ratings.Where(r => uid.Contains(r.UserID)).ToList();

            Parallel.ForEach(bookDetails.ratings, rating => {
                if (users.Contains(rating.UserID))
                {
                    ratings.Add(rating);
                }
            });

            //Parallel.ForEach(ratings, rating =>
            foreach (BookUserRating rating in ratings)
            {
                if (!bookRatings.ContainsKey(rating.ISBN))
                {
                    bookRatings.Add(rating.ISBN, new List<int>());
                }
                bookRatings[rating.ISBN].Add(rating.Rating);
            }//);
            
            return bookRatings;
        }

        public static AgeGroup GetAgeGroup(int age)
        {
            var ranges = Enum.GetValues(typeof(AgeGroup));

            foreach(AgeGroup range in ranges)
            {
                if (age <= (int)range)
                    return range;
            }
            return AgeGroup.SeniorCitizens;
        }
    }
}
