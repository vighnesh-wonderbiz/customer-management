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
                order.CurrentOrderDetails = createOrderDTO.CurrentOrderDetails.Select(x => new OrderDetail()
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    ProductPrice = products[x.ProductId].ProductPrice,
                    ProdcutSubtotal = products[x.ProductId].ProductPrice * x.Quantity,
                    ProductTotal = products[x.ProductId].ProductPrice * x.Quantity + products[x.ProductId].IGstRate + products[x.ProductId].CGstRate + products[x.ProductId].SGstRate + products[x.ProductId].UTGstRate,
                    StartDate = x.StartDate != null ? x.StartDate : null,
                    EndDate = x.StartDate != null ? x.StartDate : null,
                    CreatedDate = DateTimeOffset.Now,
                    UpdatedDate = DateTimeOffset.Now,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy
                }).ToList();
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

        public Task<bool> DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.FindAllAsync();
            var mappedOrder = orders.Select(x => orderRepository.MapOrders(x));
            return mappedOrder;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await orderRepository.FindByIdAsync(id);
            var mappedOrder = orderRepository.MapOrders(order);
            return mappedOrder;
        }

        public async Task<OrderDTO> UpdateOrderAsync(int id, UpdateOrderDTO updateOrderDTO)
        {
            if (id != updateOrderDTO.OrderId) throw new Exception("Invalid id in request body");
            var oldOrder = await orderRepository.FindByIdAsync(id);
            if (oldOrder != null)
            {
                var oldOrderDTO = mapper.Map<UpdateOrderDTO>(oldOrder);
                var updateRequestDTO = mapper.Map(updateOrderDTO, oldOrder);

                var updateRequest = mapper.Map<Order>(updateRequestDTO);
                updateRequest.UpdatedDate = DateTimeOffset.Now;
                updateRequest.CreatedDate = oldOrder.CreatedDate;
                updateRequest.CreatedBy = oldOrder.CreatedBy;

                var updatedOrder = await orderRepository.UpdateAsync(updateRequest);
                var populateOrder = await orderRepository.FindByIdAsync(updatedOrder.OrderId);
                var mappedOrder = orderRepository.MapOrders(populateOrder);
                return mappedOrder;
            }
            else
            {
                throw new InvalidOperationException($"Order with ID {id} not found.");
            }
        }
    }
}
