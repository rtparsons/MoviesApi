using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(6)]
        public string Name { get; set; }
    }
}
