using KeithFinch.Models;
using KeithFinch.Repositories.Interfaces;
using KeithFinch.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeithFinch.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();
            return users;
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);

            return user;
        }

        public async Task<UserModel> CreateUser(User model)
        {
            return await _userRepository.CreateUser(model);
        }

        public async Task<UserModel> UpdateUser(UserModel model)
        {
             return await _userRepository.UpdateUser(model);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
