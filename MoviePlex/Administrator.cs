﻿using System;
using System.Linq;

namespace MoviePlex
{
    public static class Administrator
    {

        readonly static int maxMovies = 10;

        static int ReadMovieDetails(int movieCounter)
        {
            var movie = new MovieViewModel();
            string movieName, rating;

            Console.WriteLine($"Please Enter {GetNumberInWord(movieCounter + 1)} movie's name:");
            movieName = Console.ReadLine();

            Console.WriteLine($"Please enter Age limit or Rating for {GetNumberInWord(movieCounter + 1) } movie");
            rating = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(movieName) && !string.IsNullOrWhiteSpace(rating))
            {
                movie.Name = movieName;
                if ((UtilityFunctions.IsNumber(rating) && Convert.ToInt32(rating) > 0 && Convert.ToInt32(rating) < 100) || Movie.MovieRating.ContainsKey(rating))
                {

                    movie.Restriction = rating;
                    MovieOperations.AddMovie(movie);
                    return ++movieCounter;
                }
                else
                {
                    UtilityFunctions.PrintErrorMsg("Please enter valid age or rating!! ");
                    UtilityFunctions.PrintInfoMsg($"Age can be between 1 to 100 and Rating can be from {string.Join(',', Movie.MovieRating.Keys.ToList())}");
                }

                return movieCounter;
            }
            else
            {
                UtilityFunctions.PrintErrorMsg("Movie Name or Age can not be empty!!");
                return movieCounter;
            }
        }

        static string GetNumberInWord(int number)
        {
            switch (number)
            {
                case 1:
                    return "First";
                case 2:
                    return "Second";
                case 3:
                    return "Third";
                case 4:
                    return "Fourth";
                case 5:
                    return "Fifth";
                case 6:
                    return "Sixth";
                case 7:
                    return "Seventh";
                case 8:
                    return "Eighth";
                case 9:
                    return "Ninth";
                case 10:
                    return "Tenth";
            }

            return "";
        }
        static void InsertMovies()
        {

            Console.WriteLine("How many movies are playing today? (No more than 10) ");
            string noOfMoviePlaying = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(noOfMoviePlaying) && UtilityFunctions.IsNumber(noOfMoviePlaying) && Convert.ToInt32(noOfMoviePlaying) <= maxMovies)
            {
                int movieCounter = 0;

                while (movieCounter < Convert.ToInt32(noOfMoviePlaying))
                {
                    movieCounter = ReadMovieDetails(movieCounter);
                }

            askAgain:

                MovieOperations.DisplayMovies();
                Console.WriteLine("Your movies playing today are listed as above! Are you satisfied? (Y/N)? ");


                var userSelection = Console.ReadLine();

                if (userSelection.ToUpper() == "Y")
                {
                    UtilityFunctions.StartApplication();
                }
                else if (userSelection.ToUpper() == "N")
                {
                    MovieOperations.ClearMovies();
                    InsertMovies();
                }
                else
                {
                    UtilityFunctions.PrintErrorMsg("Please enter valid input!! \n");
                    goto askAgain;
                }
            }
            else
            {
                UtilityFunctions.PrintErrorMsg("No of Movies must be a number and less than 11.");
                InsertMovies();
            }

        }
        public static void ValidateAdmin()
        {
            int passwordAttemptCounter = 1;

            Console.WriteLine("Please Enter Admin Password or Press B to go back to main screen!");

            while (passwordAttemptCounter != 6)
            {
                var password = Console.ReadLine();

                if (password == "B")
                {
                    break;
                }
                else if (CheckPassword(password))
                {
                    InsertMovies();
                }
                else
                {
                    UtilityFunctions.PrintErrorMsg("Invalid Password!!");
                    UtilityFunctions.PrintErrorMsg($"You have {5 - passwordAttemptCounter} attempts left!");

                    Console.WriteLine("Enter Password again or Press B to go back to main screen!");
                }

                passwordAttemptCounter++;
            }

            UtilityFunctions.StartApplication();
        }

        private static bool CheckPassword(string password)
        {
            return (!string.IsNullOrWhiteSpace(password) && string.Equals(password, "Abcd1234")) ? true : false;
        }
    }
}
