using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data
{
    public interface IUserRepository
    {
        bool UserExistsForId(int id);
    }
}
