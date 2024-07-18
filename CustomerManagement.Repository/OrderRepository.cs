using CustomerManagement.Data;
using CustomerManagement.DTO;
using CustomerManagement.IRepositories;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(CustomerManagementDbContext _context) : base(_context)
        {
        }

        public OrderDTO MapOrders(Order order)
        {
            return new OrderDTO(
                order.OrderTotal,
                order.Discount,
                order.Balance,
                order.BalanceReminder,
                order.StartDate,
                order.EndDate,
                new UserDTO(
                    order.OrderOfUser.UserName,
                    order.OrderOfUser.Phone,
                    order.OrderOfUser.Email,
                    order.OrderOfUser.Address,
                    order.OrderOfUser.UserRole.RoleName,
                    order.OrderOfUser.UserGender.GenderName,
                    order.OrderOfUser.CreatedDate
                ),
                order.CurrentOrderDetails.Select(y => new OrderDetailDTO(
                    y.Quantity,
                    y.ProductId,
                    y.ProductPrice,
                    y.ProdcutSubtotal,
                    y.ProductTotal,
                    y.Discount,
                    y.IGst,
                    y.CGst,
                    y.SGst,
                    y.UTGst,
                    new ProductOrderDetail(
                        y.OrderDetailOfProduct.IsSubscribable,
                        y.OrderDetailOfProduct.ProductName,
                        y.OrderDetailOfProduct.ProductDescription,
                        y.OrderDetailOfProduct.IsActive,
                        y.OrderDetailOfProduct.CreatedDate
                    ),
                    y.StartDate,
                    y.EndDate,
                    y.CreatedDate
                )),
                order.CreatedDate
            );
        }
    }
}
