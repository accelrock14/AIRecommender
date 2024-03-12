using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace AIRecommender.DataLoader
{
    public class CSVDataLoader : IDataLoader
    {
        public BookDetails Load()
        {
            BookDetails bookDetails = new BookDetails();
            string userFile = ConfigurationManager.AppSettings["USER"]; // "C:\\Users\\Jeferson x\\OneDrive\\Documents\\Notes\\Case-study\\Data\\BX-Users.csv";
            string bookFile = ConfigurationManager.AppSettings["BOOK"]; // "C:\\Users\\Jeferson x\\OneDrive\\Documents\\Notes\\Case-study\\Data\\BX-Books.csv";
            string ratingFile = ConfigurationManager.AppSettings["RATE"]; // "C:\\Users\\Jeferson x\\OneDrive\\Documents\\Notes\\Case-study\\Data\\BX-Book-Ratings.csv";

            Task loadUser = new Task(() =>
            {
                try
                {
                    using (StreamReader reader = new StreamReader(userFile))
                    {
                        string lines;
                        while ((lines = reader.ReadLine()) != null)
                        {
                            List<string> detail = lines.Split(';').Select(s => s.Replace("\"", "")).ToList();
                            List<string> location = detail[1].Split(',').ToList();

                            User user = new User
                            {
                                UserID = int.TryParse(detail[0], out int id) ? id : 0,
                                Age = int.TryParse(detail.Last(), out int age) ? age : 0,
                                City = location[0].Trim(),
                                State = location.Count > 1 ? location[1].Trim() : null,
                                Country = location.Last().Trim()
                            };

                            bookDetails.users.Add(user);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            });
            Task loadBook = new Task(() =>
            {
                try
                {
                    using (StreamReader reader = new StreamReader(bookFile))
                    {
                        string lines;
                        while ((lines = reader.ReadLine()) != null)
                        {
                            List<string> detail = lines.Split(';').Select(s => s.Replace("\"", "")).ToList();

                            Book book = new Book
                            {
                                ISBN = detail[0],
                                BookTitle = detail[1],
                                BookAuthor = detail[2],
                                YearOfPublication = int.TryParse(detail[3], out int y) ? y : 0,
                                Publisher = detail[4],
                                ImageUrlSmall = detail[5],
                                ImageUrlMedium = detail[6],
                                ImageUrlLarge = detail[7]
                            };

                            bookDetails.books.Add(book);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            });
            Task loadRating = new Task(() =>
            {
                try
                {
                    using (StreamReader reader = new StreamReader(ratingFile))
                    {
                        string lines;
                        while ((lines = reader.ReadLine()) != null)
                        {
                            List<string> detail = lines.Split(';').Select(s => s.Replace("\"", "")).ToList();

                            BookUserRating rating = new BookUserRating
                            {
                                ISBN = detail[1],
                                UserID = int.TryParse(detail[0], out int id) ? id : 0,
                                Rating = int.TryParse(detail[2], out int r) ? r : 0
                            };

                            bookDetails.ratings.Add(rating);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            });


            loadUser.Start();
            loadBook.Start();
            loadRating.Start();

            loadUser.Wait();
            loadBook.Wait();
            loadRating.Wait();

            return bookDetails;
            /*var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                IgnoreBlankLines = false
            };

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    User user = new User();
                    user.Age = csv.GetField("Age")=="NULL"? 0: csv.GetField<int>("Age");
                    user.UserID = csv.GetField<int>("UserID");
                    user.City = csv.GetField("City");
                    user.State = csv.GetField("State");
                    user.Country = csv.GetField("Country");

                    Console.WriteLine(user.Age + " " + user.UserID + " " + user.Country);
                }
            }

            return bookDetails;*/
        }
    }
}
