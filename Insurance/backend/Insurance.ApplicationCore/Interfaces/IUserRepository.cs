using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.ApplicationCore.Models;

namespace Insurance.ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> GetAll();

        Task<User> GetByUsername(string Username);
    }
}
