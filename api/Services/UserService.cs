using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lapis.API.Models;
using MongoDB.Driver;

namespace Lapis.API.Services
{
    public interface IUserService
    {
        Task<ICollection<User>> GetAll();
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task<User> Create(User user);
        Task Update(string id, User user);
        Task Delete(string id);
    }

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _user;

        private string USERS_COLLECTION = "Users";

        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.GetConnectionString());
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>(USERS_COLLECTION);
        }

        /// <summary>
        /// Get all user in DB
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<User>> GetAll()
        {
            return await _user.Find(s => true).ToListAsync();
        }

        /// <summary>
        /// Get user usng Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(string id)
        {
            return await _user.Find<User>(s => s.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get user by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            return await _user.Find<User>(s => s.Email == email).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Input new user to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Create(User user)
        {
            await _user.InsertOneAsync(user);
            return user;
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(string id, User user)
        {
            user.Id = id;
            await _user.ReplaceOneAsync(s => s.Id == id, user);
        }

        /// <summary>
        /// Delete user usng id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            await _user.DeleteOneAsync(s => s.Id == id);
        }
    }
}