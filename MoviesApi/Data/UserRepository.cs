using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data
{
    public class UserRepository : IUserRepository
    {
        private Data.MovieDbContext _context;
        public UserRepository(Data.MovieDbContext context)
        {
            _context = context;
        }

        public bool UserExistsForId(int id)
        {
            return _context.Users.Any(x => x.UserId == id);
        }
    }
}
