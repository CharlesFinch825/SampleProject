using Dapper;
using KeithFinch.Models;
using KeithFinch.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeithFinch.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("SampleProjectDb");
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            IEnumerable<UserModel> users;
            string sql = $@"SELECT * FROM Users";

            using (var db = new SqlConnection(_connectionString))
            {
                users = await db.QueryAsync<UserModel>(sql, null);
            }

            return users;
        }

        public async Task<UserModel> GetUserById(Guid id)
        {
            var user = (UserModel)null;
            string sql = $@"SELECT * FROM Users WHERE Id = @Id";

            using (var db = new SqlConnection(_connectionString))
            {
                user = await db.QuerySingleAsync<UserModel>(sql, new { Id = id });
            }

            return user;
        }

        public async Task<UserModel> CreateUser(User model)
        {
            Guid newId = Guid.NewGuid();
            string sql = $@"INSERT INTO Users (Id, Email, Enabled)
                            VALUES (@Id, @Email, @Enabled)";

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sql, new { Id = newId, Email = model.Email, Enabled = model.Enabled });
            }

            return await GetUserById(newId);
        }

        public async Task<UserModel> UpdateUser(UserModel model)
        {
            string sql = $@"UPDATE Users 
                            SET Email = @Email, Enabled = @Enabled
                            WHERE Id = @Id";

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sql, new { Id = model.Id, Email = model.Email, Enabled = model.Enabled });
            }

            return await GetUserById(model.Id);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var success = false;

            string sql = $@"DELETE FROM Users
                            WHERE Id = @Id";

            using (var db = new SqlConnection(_connectionString))
            {
                var recordCount = await db.ExecuteAsync(sql, new { Id = id });
                success = recordCount == 1;
            }

            return success;
        }
    }
}
