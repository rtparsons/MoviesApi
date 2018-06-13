using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        public string Genre { get; set; }
    }
}
