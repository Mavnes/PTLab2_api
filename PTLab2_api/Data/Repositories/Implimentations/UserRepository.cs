using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Repositories.Interfaces;

namespace PTLab2_api.Data.Repositories.Implimentations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShopDbContext context) : base(context) { }

        public User? GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) 
            {
                return null;
            }

            return _context.Users.SingleOrDefault(user => user.Email.Equals(email));
        }
    }
}
