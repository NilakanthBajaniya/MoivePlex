using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MoviePlex
{
    public static class User
    {
        static bool VerifyUserAgeByRating(string rating, int userAge)
        {
            var minAge = Movie.MovieRating[rating];

            if (userAge > minAge) return true;
            else return false;
        }
        static bool VerifyAge(int movieId, int age)
        {
            var movie = MovieOperations.ReadMovieById(movieId);

            if (movie != null)
            {

                if (movie.AgeLimit.HasValue)
                {
                    if (movie.AgeLimit.Value <= age)
                        return true;
                    else
                        return false;
                }
                else if (!String.IsNullOrWhiteSpace(movie.Rating))
                {
                    return VerifyUserAgeByRating(movie.Rating, age);
                }
            }

            return false;
        }
        public static void InitGuest()
        {
            int numberOfMoviesToday = MovieOperations.AvailableMovieCount();
            MovieOperations.DisplayMovies();

            if (MovieOperations.AvailableMovieCount() == 0)
            {
                UtilityFunctions.PrintErrorMsg("No Movies Right now. Please check later!!");
                return;
            }

            while (true)
            {
                Console.Write("Which movie would you like to Watch ?");
                var choice = Console.ReadLine();

                Console.WriteLine();
                if (UtilityFunctions.IsNumber(choice) && numberOfMoviesToday >= Convert.ToInt32(choice))
                {
                    Console.Write("Please Enter Your Age For verification: ");
                    var age = Console.ReadLine();

                    if (UtilityFunctions.IsNumber(age) && Convert.ToInt32(age) > 0 && MovieOperations.AvailableMovieCount() >= Convert.ToInt32(choice))
                    {
                        if (VerifyAge(Convert.ToInt32(choice), Convert.ToInt32(age)))
                        {
                            Console.WriteLine("Enjoy Movie!");

                            Console.WriteLine("Press M to go back to Guest Menu");
                            Console.WriteLine("Press S to go back to Start Page");

                            string finalChoice = Console.ReadLine();

                            if(finalChoice.ToUpper().Equals("M"))
                            {
                                Console.Clear();
                                MovieOperations.DisplayMovies();
                            }
                            else if(finalChoice.ToUpper().Equals("S"))
                            {
                                UtilityFunctions.RestartApplication();
                            }
                            else
                            {
                                UtilityFunctions.PrintErrorMsg("Retry Again!!");
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else//checking age input
                    {
                        UtilityFunctions.PrintErrorMsg("Age: Please enter valid input!");
                        continue;
                    }
                }
                else
                {
                    UtilityFunctions.PrintErrorMsg("Movie:Please enter valid input!");
                    continue;
                }
            }
        }
    }
}
