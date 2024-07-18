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
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CustomerManagementDbContext context;

        public Repository(CustomerManagementDbContext context)
        {
            this.context = context;
        }

        public async Task<T> CreateAsync(T t)
        {
            try
            {
                var addT = await context.AddAsync(t);
                var savedT = await context.SaveChangesAsync();
                return addT.Entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(T t)
        {
            try
            {
                context.Remove(t);
                int row = await context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            try
            {
                if (typeof(T) == typeof(Order))
                {
                    var populatedOrder = await context.Set<Order>()
                                        .Include(u=>u.OrderOfUser)
                                            .ThenInclude(r=>r.UserRole)
                                        .Include(u=>u.OrderOfUser)
                                            .ThenInclude(g=>g.UserGender)
                                        .Include(od => od.CurrentOrderDetails)
                                            .ThenInclude(p => p.OrderDetailOfProduct).ToListAsync();
                    return populatedOrder.Cast<T>();
                }
                var tList = await context.Set<T>().ToListAsync();
                return tList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> FindByIdAsync(int id)
        {
            try
            {
                if (typeof(T) == typeof(Order))
                {
                    var populatedOrder = await context.Set<Order>()
                                        .Include(u=>u.OrderOfUser)
                                            .ThenInclude(r=>r.UserRole)
                                        .Include(u=>u.OrderOfUser)
                                            .ThenInclude(g=>g.UserGender)
                                        .Include(od => od.CurrentOrderDetails)
                                            .ThenInclude(p => p.OrderDetailOfProduct)
                                        .FirstOrDefaultAsync(o=>o.OrderId == id);
                    return populatedOrder as T;
                }
                var t = await context.Set<T>().FindAsync(id);
                return t;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T t)
        {
            try
            {
                context.Update(t);
                await context.SaveChangesAsync();
                return t;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
