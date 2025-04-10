using KeithFinch.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeithFinch.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsers();
        Task<UserModel> GetUserById(Guid id);
        Task<UserModel> CreateUser(User model);
        Task<UserModel> UpdateUser(UserModel model);
        Task<bool> DeleteUser(Guid id);
    }
}
