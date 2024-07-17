using CustomerManagement.Data;
using CustomerManagement.IRepositories;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CustomerManagementDbContext _context;
    public UserRepository(CustomerManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _context.Users.Include(rol=>rol.UserRole).Include(gen=>gen.UserGender).ToListAsync();
        return users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _context.Users.Include(rol => rol.UserRole).Include(gen => gen.UserGender).FirstOrDefaultAsync(i => i.UserId == id);
        return user;
    }
    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            var addT = await _context.AddAsync(user);
            var savedT = await _context.SaveChangesAsync();
            var savedUser = await GetByIdAsync(addT.Entity.UserId);
            return savedUser;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<User> PutUserAsync(User user)
    {
        try
        {
                var _user = _context.Users.Update(user);
                await _context.SaveChangesAsync();
                var updateUser = await GetByIdAsync(_user.Entity.UserId);
                return updateUser;
        }
        catch (Exception e)
        {
            throw;
        }
    }

}
}
