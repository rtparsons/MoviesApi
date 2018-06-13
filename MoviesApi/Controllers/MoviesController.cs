using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private Data.IMovieRepository _movieRepository;
        private Data.IUserRepository _userRepository;

        public MoviesController(Data.IMovieRepository movieRepository,
                                Data.IUserRepository userRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        // GET api/movies/all
        [HttpGet("all")]
        public IActionResult GetAllMovies()
        {
            var movies = _movieRepository.GetAll();

            if (movies.Count > 0) return Ok(movies);
            return NotFound("No movies found");
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie != null) return Ok(movie);
            return BadRequest("No movie exists for this id"); 
        }

        // GET api/movies
        [HttpGet]
        public IActionResult GetMoviesByFilter(string title, int yearOfRelease, string genre)
        {
            if(string.IsNullOrEmpty(title) &&
                string.IsNullOrEmpty(genre) &&
                yearOfRelease == 0)
            {
                return BadRequest("Please enter at least one search criteria");
            }

            var movies = _movieRepository.GetMoviesByFilter(title, yearOfRelease, genre);
            if (movies.Count > 0) return Ok(movies);
            return NotFound("No movies found");
        }

        // GET api/movies/top5
        [HttpGet("top5")]
        public IActionResult GetTop5MoviesByAverageUserRating()
        {
            var movies = _movieRepository.GetAllMoviesWithAverageUserRating().Take(5).ToList();
            if (movies.Count > 0) return Ok(movies);
            return NotFound("No movies found");
        }

        // GET api/movies/user/1
        [HttpGet("user/{userId}")]
        public IActionResult GetTop5MoviesForUserByRating(int userId)
        {
            if(!_userRepository.UserExistsForId(userId))
            {
                return BadRequest("No user exists for this id.");
            }

            var movies = _movieRepository.GetAllMoviesRatedByUser(userId).Take(5).ToList();
            if (movies.Count > 0) return Ok(movies);
            return NotFound("No movies found");
        }

        // PUT api/movies/rate
        [HttpPut("rate")]
        public IActionResult RateMovie(Models.MovieRating movieRating)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExistsForId(movieRating.UserId))
            {
                return BadRequest("No user exists for this id.");
            }

            if (!_movieRepository.MovieExistForMovieId(movieRating.MovieId))
            {
                return BadRequest("No movie exists for this id.");
            }

            _movieRepository.RateMove(movieRating);
            return Ok();
        }
    }
}