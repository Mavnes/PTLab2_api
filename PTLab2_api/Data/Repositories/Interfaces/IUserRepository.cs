﻿using PTLab2_api.Data.Models;

namespace PTLab2_api.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByEmail(string email);
    }
}
