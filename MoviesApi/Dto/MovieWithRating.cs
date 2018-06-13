using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Dto
{
    public class MovieWithRating : Models.Movie
    {
        public decimal Rating { get; set; }
    }
}
