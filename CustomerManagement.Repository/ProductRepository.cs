using CustomerManagement.Data;
using CustomerManagement.IRepositories;
using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
        public ProductRepository(CustomerManagementDbContext context) : base(context)
        {
        }
    }
}
