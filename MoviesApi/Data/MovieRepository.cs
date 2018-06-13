using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data
{
    public class MovieRepository : IMovieRepository
    {
        private Data.MovieDbContext _context;
        public MovieRepository(Data.MovieDbContext context)
        {
            _context = context;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.FirstOrDefault(x => x.MovieId == id);
        }

        public List<Models.Movie> GetMoviesByFilter(string title, int yearOfRelease, string genre)
        {
            Func<Movie, bool> titleFilter = (Movie x) => title == null || x.Title.Contains(title);
            Func<Movie, bool> yearFilter = (Movie x) => yearOfRelease == 0 || x.YearOfRelease == yearOfRelease;
            Func<Movie, bool> genreFilter = (Movie x) => genre == null || x.Genre == genre;

            return _context.Movies.Where(x => titleFilter(x) &&
                                            yearFilter(x) &&
                                            genreFilter(x)).ToList();
        }

        public List<Dto.MovieWithRating> GetAllMoviesWithAverageUserRating()
        {
            var moviesWithAverage = _context.MovieRatings
                                            .Join(_context.Movies,
                                                    movieRating => movieRating.MovieId,
                                                    movie => movie.MovieId,
                                                    (movieRating, movie) => new { movie, movieRating })
                                            .GroupBy(x => x.movieRating.MovieId)
                                            .Select(x =>  new Dto.MovieWithRating
                                            {
                                                Rating = RoundToPoint5(x.Sum(y => (decimal)y.movieRating.Rating) / (decimal)x.Count()),
                                                Genre = x.First().movie.Genre,
                                                MovieId = x.First().movie.MovieId,
                                                Title = x.First().movie.Title,
                                                YearOfRelease = x.First().movie.YearOfRelease
                                            });
            return moviesWithAverage.OrderByDescending(x => x.Rating)
                                    .ThenBy(x => x.Title)
                                    .ToList();
        }

        public List<Dto.MovieWithRating> GetAllMoviesRatedByUser(int userId)
        {
            var userMovies = _context.MovieRatings
                                .Join(_context.Movies,
                                        movieRating => movieRating.MovieId,
                                        movie => movie.MovieId,
                                        (movieRating, movie) => new { movie, movieRating })
                                .Where(x => x.movieRating.UserId == userId)
                                .Select(x => new Dto.MovieWithRating
                                {
                                    Rating = (decimal)x.movieRating.Rating,
                                    Genre = x.movie.Genre,
                                    MovieId = x.movie.MovieId,
                                    Title = x.movie.Title,
                                    YearOfRelease = x.movie.YearOfRelease
                                });

            return userMovies.OrderByDescending(x => x.Rating)
                            .ThenBy(x => x.Title)
                            .ToList();
        }

        public void RateMove(MovieRating movieRating)
        {
            var existingEntry = _context.MovieRatings.AsNoTracking().FirstOrDefault(x => x.MovieId == movieRating.MovieId &&
                                                                          x.UserId == movieRating.UserId);

            if (existingEntry != null)
            {
                _context.Update(movieRating);
            }
            else {
                _context.Add(movieRating);
            }

            _context.SaveChanges();
        }

        public bool MovieExistForMovieId(int id)
        {
            return _context.Movies.Any(x => x.MovieId == id);
        }

        private decimal RoundToPoint5(decimal toRound)
        {
            return Math.Round(toRound * 2m, MidpointRounding.AwayFromZero) / 2m; 
        }
    }
}
