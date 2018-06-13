using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApi.Dto;
using MoviesApi.Models;

namespace MoviesApi.Data
{
    public interface IMovieRepository
    {
        List<Models.Movie> GetAll();
        Models.Movie GetById(int id);
        List<Models.Movie> GetMoviesByFilter(string title, int yearOfRelease, string genre);
        List<Dto.MovieWithRating> GetAllMoviesWithAverageUserRating();
        List<Dto.MovieWithRating> GetAllMoviesRatedByUser(int userId);
        void RateMove(MovieRating movieRating);
        bool MovieExistForMovieId(int id);
    }
}
