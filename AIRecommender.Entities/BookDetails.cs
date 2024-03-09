using System.Collections.Generic;

namespace AIRecommender.DataLoader
{
    public class BookDetails
    {
        public List<Book> books = new List<Book>();
        public List<BookUserRating> ratings = new List<BookUserRating>();
        public List<User> users = new List<User>();
    }
}
