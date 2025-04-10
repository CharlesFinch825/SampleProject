using KeithFinch.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeithFinch.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUser(Guid id);
        Task<UserModel> CreateUser(User model);
        Task<UserModel> UpdateUser(UserModel model);
        Task<bool> DeleteUser(Guid id);
    }
}
