using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRating>()
                .HasKey(k => new { k.MovieId, k.UserId });
        }
    }
}
