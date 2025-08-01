using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.ApplicationCore.Interfaces;
using Insurance.ApplicationCore.Models;


namespace Insurance.Infrastructure.Services
{
    public class Authservice
    {
        private readonly IUserRepository _userRepository;
        public Authservice(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user == null)
                return false;
            // Assuming you have a method to verify the password
            return password != null && password == user.Password;
        }

        public async Task<User> Register(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Invalid user data");
            var existingUser = await _userRepository.GetByUsername(user.Username);
            if (existingUser != null)
                throw new InvalidOperationException("User already exists");
            return await _userRepository.Create(user);
        }
    }
}
