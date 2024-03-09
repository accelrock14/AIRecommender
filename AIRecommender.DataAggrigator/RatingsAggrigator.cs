using AIRecommender.DataLoader;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIRecommender.DataAggrigator
{
    public class RatingsAggrigator : IRatingsAggrigator
    {
        public enum AgeGroup { TeenAge, YoungAge, MidAge, OldAge, SeniorCitizens, NULL }
        public Dictionary<string, List<int>> Aggrigate(BookDetails bookDetails, Preference preferance)
        {
            Dictionary<string, List<int>> bookRatings = new Dictionary<string, List<int>>();

            List<User> users = bookDetails.users.Where(user=> user.State == preferance.State && GetAgeGroup(user.Age)==GetAgeGroup(preferance.Age)).ToList();

            List<int> uid = users.Select(user=>user.UserID).ToList();
            
            ConcurrentBag<BookUserRating> ratings = new ConcurrentBag<BookUserRating>();// = bookDetails.ratings.Where(r => uid.Contains(r.UserID)).ToList();

            Parallel.ForEach(bookDetails.ratings, rating => {
                if(uid.Contains(rating.UserID))
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
            /*
             Parallel.ForEach(bookDetails.books, book =>
             {
                 List<int> ratings = bookDetails.ratings.Where(s => s.ISBN == book.ISBN && users.Select(u => u.UserID).Contains(s.UserID)).Select(s => s.Rating).ToList();
                 if (ratings.Any())
                     bookRatings[book.ISBN] = ratings;
             });
             foreach(BookUserRating rating in ratings)
             {
                 if (!bookRatings.ContainsKey(rating.ISBN))
                 {
                     bookRatings[rating.ISBN] = new List<int>();
                 }
                 bookRatings[rating.ISBN].Add(rating.Rating);
                 Console.WriteLine(rating.Rating);
             }*/

            return bookRatings;
        }
        
        public static AgeGroup GetAgeGroup(int age)
        {
            if(age <= 0)
                return AgeGroup.NULL;
            else if (age < 17)
                return AgeGroup.TeenAge;
            else if(age < 31)
                return AgeGroup.YoungAge;
            else if( age < 51)
                return AgeGroup.MidAge;
            else if(age < 61)
                return AgeGroup.OldAge;

            return AgeGroup.SeniorCitizens;
        }
    }
}
