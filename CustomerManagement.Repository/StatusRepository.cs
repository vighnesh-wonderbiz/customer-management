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

    public class StatusRepository : Repository<User>, IStatusRepository
    {
        private readonly CustomerManagementDbContext _context;
        public StatusRepository(CustomerManagementDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
