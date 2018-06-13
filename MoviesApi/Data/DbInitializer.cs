using MoviesApi.Models;
using System;
using System.Linq;

namespace MoviesApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MovieDbContext context)
        {
            context.Database.EnsureCreated();

            // Check the db hasn't already been seeded
            if (context.Movies.Any())
            {
                return; 
            }

            SeedMoviesTableWithDummyData(context);
            SeedUsersTableWithDummyData(context);
            SeedMovieRatingsTableWithDummyData(context);
        }

        private static void SeedMoviesTableWithDummyData(MovieDbContext context)
        {
            var movies = new Movie[]
            {
                new Movie{Title="Die Hard", Genre="Action", YearOfRelease=1990},
                new Movie{Title="Die Hard 2", Genre="Action", YearOfRelease=1993},
                new Movie{Title="Shaun of the dead", Genre="Comedy", YearOfRelease=2010},
                new Movie{Title="Hot Fuzz", Genre="Comedy", YearOfRelease=2012},
                new Movie{Title="The Dark Knight", Genre="Action", YearOfRelease=2010},
                new Movie{Title="Halloween", Genre="Horror", YearOfRelease=1980},
                new Movie{Title="Deadpool", Genre="Action", YearOfRelease=2015},
                new Movie{Title="The Matrix", Genre="Action", YearOfRelease=2000},
                new Movie{Title="Love Actually", Genre="Romance", YearOfRelease=2000}
            };

            foreach (Movie m in movies)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();
        }

        private static void SeedUsersTableWithDummyData(MovieDbContext context)
        {
            var users = new User[]
            {
                new User{Name="Al"},
                new User{Name="Bob"},
                new User{Name="Carla"},
                new User{Name="Dave"},
                new User{Name="Eugine"},
                new User{Name="Fran"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        private static void SeedMovieRatingsTableWithDummyData(MovieDbContext context)
        {
            var ratings = new MovieRating[]
            {
                new MovieRating{MovieId=1, UserId=1, Rating=1},
                new MovieRating{MovieId=2, UserId=2, Rating=2},
                new MovieRating{MovieId=3, UserId=3, Rating=3},
                new MovieRating{MovieId=4, UserId=4, Rating=4},
                new MovieRating{MovieId=5, UserId=5, Rating=5},
                new MovieRating{MovieId=1, UserId=5, Rating=4},
                new MovieRating{MovieId=7, UserId=1, Rating=4},
                new MovieRating{MovieId=7, UserId=2, Rating=5},
                new MovieRating{MovieId=7, UserId=3, Rating=5}
            };

            foreach (MovieRating r in ratings)
            {
                context.MovieRatings.Add(r);
            }
            context.SaveChanges();
        }

    }
}