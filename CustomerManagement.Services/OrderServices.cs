using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CustomerManagement.DTO;
using CustomerManagement.IRepositories;
using CustomerManagement.IServices;
using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository _productRepository;
        public OrderServices(IMapper _mapper, IOrderRepository _orderRepository, IProductRepository productRepository)
        {
            mapper = _mapper;
            orderRepository = _orderRepository;
            _productRepository = productRepository;
        }

        public async Task<CreatedOrderDTO> PostOrderAsync(CreateOrderDTO createOrderDTO)
        {
            try
            {
                var productIds = createOrderDTO.CurrentOrderDetails.Select(p => p.ProductId).ToList();
                var products = await _productRepository.FetchProductsByIdAsync(productIds);

                Order order = new Order();
                order.Balance = createOrderDTO.Balance;
                var currentOrderDetails = new List<OrderDetail>();
                foreach (var x in createOrderDTO.CurrentOrderDetails)
                {
                    var orderDetail = new OrderDetail();
                    var currentProduct = products[x.ProductId];
                    orderDetail.ProductId = x.ProductId;
                    orderDetail.Quantity = x.Quantity;
                    orderDetail.ProductPrice = currentProduct.ProductPrice;
                    orderDetail.ProdcutSubtotal = currentProduct.ProductPrice * x.Quantity;
                    var orderTotal = orderDetail.ProdcutSubtotal + currentProduct.IGstRate + currentProduct.CGstRate + currentProduct.SGstRate + currentProduct.UTGstRate;
                    var discountedPrice = orderDetail.ProdcutSubtotal * (x.Discount / 100);
                    orderDetail.Discount = x.Discount;
                    orderDetail.ProductTotal = orderTotal - discountedPrice;
                    orderDetail.StartDate = x.StartDate != null ? x.StartDate : null;
                    orderDetail.EndDate = x.StartDate != null ? x.StartDate : null;
                    orderDetail.CreatedDate = DateTimeOffset.Now;
                    orderDetail.UpdatedDate = DateTimeOffset.Now;
                    orderDetail.CreatedBy = x.CreatedBy;
                    orderDetail.UpdatedBy = x.CreatedBy;
                    currentOrderDetails.Add(orderDetail);
                }
                order.CurrentOrderDetails = currentOrderDetails;
                order.OrderTotal = order.CurrentOrderDetails.Sum(p => p.ProductTotal);
                order.Balance = order.OrderTotal;
                order.Discount = order.CurrentOrderDetails.Sum(p => p.Discount);
                order.BalanceReminder = DateTimeOffset.Now;
                order.UserId = createOrderDTO.UserId;
                order.CreatedDate = DateTimeOffset.Now;
                order.UpdatedDate = DateTimeOffset.Now;
                order.CreatedBy = createOrderDTO.CreatedBy;
                order.UpdatedBy = createOrderDTO.UpdatedBy;

                var createdOrder = await orderRepository.CreateAsync(order);

                var mappedOrder = new CreatedOrderDTO
                (
                    createdOrder.OrderTotal,
                    createdOrder.Discount,
                    createdOrder.Balance,
                    createdOrder.BalanceReminder,
                    createdOrder.StartDate,
                    createdOrder.EndDate,
                    createdOrder.CurrentOrderDetails.Select(x => new OrderDetailDTO(
                        x.Quantity,
                        x.ProductId,
                        x.ProductPrice,
                        x.ProdcutSubtotal,
                        x.ProductTotal,
                        x.Discount,
                        x.IGst,
                        x.CGst,
                        x.SGst,
                        x.UTGst,
                        new ProductOrderDetail(
                           products[x.ProductId].IsSubscribable,
                           products[x.ProductId].ProductName,
                           products[x.ProductId].ProductDescription,
                           products[x.ProductId].IsActive,
                           products[x.ProductId].CreatedDate
                        ),
                        x.StartDate,
                        x.EndDate,
                        x.CreatedDate
                    )),
                    createdOrder.CreatedDate
                );
                return mappedOrder;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await orderRepository.FindByIdAsync(id);
            if (order != null)
            {
                var isDeleted = await orderRepository.DeleteAsync(order);
                return isDeleted;
            }
            return false;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await orderRepository.FindAllAsync();
                var mappedOrder = orders.Select(x => orderRepository.MapOrders(x));
                return mappedOrder;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await orderRepository.FindByIdAsync(id);
                if (order != null)
                {
                    var mappedOrder = orderRepository.MapOrders(order);
                    return mappedOrder;
                }
                throw new Exception($"No Order with Id {id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OrderDTO> UpdateOrderAsync(int id, UpdateOrderDTO updateOrderDTO)
        {
            try
            {
                var oldOrder = await orderRepository.FindByIdAsync(id);
                var orderDetails = await orderRepository.GetOrderDetailsOfOrder(id);
                if (oldOrder != null)
                {
                    var productsId = orderDetails.Values.Select(p => p.ProductId);
                    var oldOrderProductsId = oldOrder.CurrentOrderDetails.Select(p => p.ProductId).ToList();
                    if (productsId != null && oldOrderProductsId != null &&
                    productsId.OrderBy(x => x).SequenceEqual(oldOrderProductsId.OrderBy(x => x)))
                    {
                        var products = await _productRepository.FetchProductsByIdAsync(productsId);
                        Order order = new Order();
                        order.OrderId = updateOrderDTO.OrderId;
                        var updatedOrderDetails = new List<OrderDetail>();
                        foreach (var x in updateOrderDTO.CurrentOrderDetails)
                        {
                            var orderDetail = new OrderDetail();
                            var currentOrder = orderDetails[x.OrderDetailsId];
                            orderDetail.OrderDetailsId = x.OrderDetailsId;
                            orderDetail.ProductId = x.ProductId;
                            orderDetail.Quantity = x.Quantity;
                            orderDetail.ProductPrice = currentOrder.OrderDetailOfProduct.ProductPrice;
                            orderDetail.ProdcutSubtotal = currentOrder.OrderDetailOfProduct.ProductPrice * x.Quantity;
                            var orderTotal = orderDetail.ProdcutSubtotal + orderDetail.IGst + orderDetail.CGst + orderDetail.SGst + orderDetail.UTGst;
                            var discountedPrice = orderDetail.ProdcutSubtotal * (x.Discount / 100);
                            orderDetail.Discount = x.Discount;
                            orderDetail.IGst = x.IGst != 0 ? x.IGst : currentOrder.OrderDetailOfProduct.IGstRate;
                            orderDetail.CGst = x.CGst != 0 ? x.CGst : currentOrder.OrderDetailOfProduct.CGstRate;
                            orderDetail.SGst = x.SGst != 0 ? x.SGst : currentOrder.OrderDetailOfProduct.SGstRate;
                            orderDetail.ProductTotal = orderTotal - discountedPrice;
                            orderDetail.StartDate = x.StartDate ?? currentOrder.OrderDetailOfProduct.StartDate;
                            orderDetail.EndDate = x.EndDate ?? currentOrder.OrderDetailOfProduct.EndDate;
                            orderDetail.CreatedDate = currentOrder.OrderDetailOfProduct.CreatedDate;
                            orderDetail.UpdatedDate = x.UpdatedDate;
                            orderDetail.CreatedBy = currentOrder.OrderDetailOfProduct.CreatedBy;
                            orderDetail.UpdatedBy = x.UpdatedBy;
                            updatedOrderDetails.Add(orderDetail);
                        }
                        order.CurrentOrderDetails = updatedOrderDetails;
                        order.OrderTotal = order.CurrentOrderDetails.Sum(p => p.ProductTotal);
                        order.Balance = updateOrderDTO.Balance;
                        order.Discount = order.CurrentOrderDetails.Sum(p => p.Discount);
                        order.BalanceReminder = updateOrderDTO.BalanceReminder ?? DateTimeOffset.Now;
                        order.UserId = updateOrderDTO.UserId;
                        order.CreatedDate = oldOrder.CreatedDate;
                        order.UpdatedDate = oldOrder.UpdatedDate;
                        order.CreatedBy = oldOrder.CreatedBy;
                        order.UpdatedBy = updateOrderDTO.UpdatedBy;
                        var createdOrder = await orderRepository.UpdateAsync(order);
                        var updatedOrder = await orderRepository.FindByIdAsync(createdOrder.OrderId);
                        var mappedOrder = orderRepository.MapOrders(updatedOrder);
                        return mappedOrder;
                    }
                    throw new Exception("Invalid Operation: Cannot update product not present in order summary.");
                }
                else
                {
                    throw new InvalidOperationException($"Order with ID {id} not found.");
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
