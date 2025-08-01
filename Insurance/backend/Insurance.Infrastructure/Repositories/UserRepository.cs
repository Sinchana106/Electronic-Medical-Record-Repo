using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.ApplicationCore.Interfaces;
using Insurance.ApplicationCore.Models;
using MongoDB.Driver;


namespace Insurance.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IMongoClient client)
        {
            var database = client.GetDatabase("InsurerDB");
            _users = database.GetCollection<User>(nameof(User));
        }
        public Task<User> Create(User user)
        {
            _users.InsertOne(user);
            return Task.FromResult(user);
        }

        public Task<IEnumerable<User>> GetAll()
        {
            var users=_users.Find(_ => true).ToListAsync().ContinueWith(task => (IEnumerable<User>)task.Result);
            return users;
        }

        public Task<User> GetByUsername(string Username)
        {
            var user = _users.Find(u => u.Username == Username).FirstOrDefaultAsync();
            return user.ContinueWith(task => task.Result);

        }
    }
}
