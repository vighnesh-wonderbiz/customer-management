using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddUserAsync(User user);

        Task<User> PutUserAsync(User user);

    }
}
