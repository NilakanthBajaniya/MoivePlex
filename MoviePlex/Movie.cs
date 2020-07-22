using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviePlex
{
    public class Movie
    {
        public static Dictionary<string, int> MovieRating = new Dictionary<string, int>
        {
            { "G" , 0},
            {"PG", 9},
            {"PG-13",12 },
            { "R", 14},
            {"NC-17", 16 }
        };

        public int MovieId { get; set; }
        public string Name { get; set; }
        public int? AgeLimit { get; set; }
        public string Rating { get; set; }

    }

    public class MovieViewModel
    {
        public string Name { get; set; }
        public string Restriction { get; set; }
    }

    public static class MovieOperations
    {
        static List<Movie> applicationMovies = new List<Movie>();

        public static Movie? ReadMovieById(int movieId)
        {
            var movie = applicationMovies.Where(x => x.MovieId == movieId).FirstOrDefault();

            return movie;
        }
        public static void AddMovie(MovieViewModel movie)
        {
            var newMovie = new Movie();

            newMovie.MovieId = applicationMovies.Count + 1;
            newMovie.Name = movie.Name;

            if (UtilityFunctions.IsNumber(movie.Restriction))
            {
                newMovie.AgeLimit = Convert.ToInt32(movie.Restriction);
            }
            else
            {
                newMovie.Rating = movie.Restriction;
            }

            applicationMovies.Add(newMovie);
        }

        public static void ClearMovies()
        {
            applicationMovies.Clear();
        }
        public static void DisplayMovies()
        {

            for (int i = 0; i < applicationMovies.Count; i++)
            {
                string printString = (applicationMovies[i].MovieId) + ". " + applicationMovies[i].Name + " {" + 
                    (applicationMovies[i].AgeLimit.HasValue ? Convert.ToString(applicationMovies[i].AgeLimit.Value) : applicationMovies[i].Rating) + "}";

                Console.WriteLine(printString);
            }

        }
        public static int AvailableMovieCount()
        {
            return applicationMovies.Count;
        }
    }
}
