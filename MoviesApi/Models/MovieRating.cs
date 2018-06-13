using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models
{
    public class MovieRating 
    {
        [ForeignKey("User")]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [ForeignKey("Movie")]
        [Column(Order = 2)]
        public int MovieId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}
