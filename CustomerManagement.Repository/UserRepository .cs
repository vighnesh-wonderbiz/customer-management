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
    /*

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CustomerManagementDbContext _context;
    public UserRepository(CustomerManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _context.Users.Include(rol=>rol.Role).Include(gen=>gen.Gender).ToListAsync();
        return users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _context.Users.Include(rol => rol.Role).Include(gen => gen.Gender).FirstOrDefaultAsync(i => i.UserId == id);
        return user;
    }
    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            var addT = await _context.AddAsync(user);
            var savedT = await _context.SaveChangesAsync();
            await _context.Entry(user).Reference(u => u.Role).LoadAsync();
            await _context.Entry(user).Reference(u => u.Gender).LoadAsync();
            return user;
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
            var updateUser = _context.Update(user);
            var updatedUser =  await _context.SaveChangesAsync();
            if (updateUser.UserId)
            {
                var populatedUser = await GetByIdAsync(updateUser.UserId);
            }
            return populatedUser;
            /*
            var addT = await _context.AddAsync(user);
            var savedT = await _context.SaveChangesAsync();
            await _context.Entry(user).Reference(u => u.Role).LoadAsync();
            await _context.Entry(user).Reference(u => u.Gender).LoadAsync();
            return user;
        }
        catch (Exception e)
        {
            throw;
        }
    }

}
            */
}
