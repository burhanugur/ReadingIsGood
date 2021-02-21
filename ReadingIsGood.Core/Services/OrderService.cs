using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Core.Model;
using ReadingIsGood.Core.Model.Order;
using ReadingIsGood.Core.Model.Stock;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Core.Services.Interfaces;
using ReadingIsGood.Domain.DomainEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services
{
    public class OrderService : IOrderService
    {
        #region Member(s)
        private readonly IOrderRepository orderRepository;
        private readonly IStockRepository stockRepository;
        #endregion

        #region Constructor(s)
        public OrderService(
            IOrderRepository orderRepository,
            IStockRepository stockRepository)
        {
            this.orderRepository = orderRepository;
            this.stockRepository = stockRepository;
        }
        #endregion

        public async Task<ServiceResponse<Guid>> CreateOrder(OrderCreateRequest request)
        {
            var order = await this.orderRepository.CreateAsync(new Domain.Order
            {
                CustomerId = request.CustomerId,
                Status = StockStatus.GettingReady,
                StockId = request.StockId
            });

            if (order == null)
            {
                return new ServiceResponse<Guid>
                {
                    IsSuccess = false,
                    Message = "The order could not be created.",
                };
            }

            var stock = await this.stockRepository.GetAsync(order.StockId);
            if (stock == null)
            {
                await this.orderRepository.DeleteAsync(order);

                return new ServiceResponse<Guid>
                {
                    Message = "Stock not found.",
                    IsSuccess = false
                };
            }

            stock.Count -= 1;
            await this.stockRepository.UpdateAsync(stock);

            return new ServiceResponse<Guid>
            {
                Result = order.Id
            };
        }

        public async Task<ServiceResponse<OrderDto>> UpdateOrderStatus(Guid id)
        {
            var order = await this.orderRepository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == id,
                include: x => x.Include(x => x.Customer).Include(x => x.Stock));

            if (order == null)
            {
                return new ServiceResponse<OrderDto>
                {
                    IsSuccess = false,
                    Message = "Order not found"
                };
            }

            order.Status = StockStatus.Delivered;
            var result = await this.orderRepository.UpdateAsync(order);

            return new ServiceResponse<OrderDto>
            {
                Result = new OrderDto
                {
                    Customer = new CustomerDto
                    {
                        CreateDate = result.Customer.CreateDate,
                        Id = result.Customer.Id,
                        ModifyDate = result.Customer.ModifyDate,
                        Name = result.Customer.Name,
                        Surname = result.Customer.Surname
                    },
                    CustomerId = result.CustomerId,
                    Status = result.Status,
                    Stock = new Model.Stock.StockDto
                    {
                        Count = result.Stock.Count,
                        CreateDate = result.Stock.CreateDate,
                        Id = result.Stock.Id,
                        ModifyDate = result.Stock.ModifyDate,
                        Name = result.Stock.Name,
                        Price = result.Stock.Price
                    },
                    StockId = result.StockId
                }
            };
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetOrderList(Guid customerId)
        {
            var list = await this.orderRepository.GetListAsync(
                predicate: x => x.CustomerId == customerId);

            return new ServiceResponse<List<OrderDto>>
            {
                Result = list.Select(x => new OrderDto
                {
                    Customer = new CustomerDto
                    {
                        CreateDate = x.Customer.CreateDate,
                        Id = x.Customer.Id,
                        ModifyDate = x.Customer.ModifyDate,
                        Name = x.Customer.Name,
                        Surname = x.Customer.Surname
                    },
                    CustomerId = x.CustomerId,
                    Status = x.Status,
                    Stock = new StockDto
                    {
                        Count = x.Stock.Count,
                        CreateDate = x.Stock.CreateDate,
                        Id = x.Stock.Id,
                        ModifyDate = x.Stock.ModifyDate,
                        Name = x.Stock.Name,
                        Price = x.Stock.Price
                    },
                    StockId = x.StockId
                }).ToList()
            };
        }

        public async Task<ServiceResponse<OrderDto>> GetOrder(Guid id)
        {
            var order = await this.orderRepository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == id,
                include: x => x.Include(x => x.Customer).Include(x => x.Stock));

            if (order == null)
            {
                return new ServiceResponse<OrderDto>
                {
                    IsSuccess = false,
                    Message = "Order not found"
                };
            }

            return new ServiceResponse<OrderDto>
            {
                Result = new OrderDto
                {
                    Customer = new CustomerDto
                    {
                        CreateDate = order.Customer.CreateDate,
                        Id = order.Customer.Id,
                        ModifyDate = order.Customer.ModifyDate,
                        Name = order.Customer.Name,
                        Surname = order.Customer.Surname
                    },
                    CustomerId = order.CustomerId,
                    Status = order.Status,
                    Stock = new StockDto
                    {
                        Count = order.Stock.Count,
                        CreateDate = order.Stock.CreateDate,
                        Id = order.Stock.Id,
                        ModifyDate = order.Stock.ModifyDate,
                        Name = order.Stock.Name,
                        Price = order.Stock.Price
                    },
                    StockId = order.StockId,
                    CreateDate = order.CreateDate,
                    Id = order.Id,
                    ModifyDate = order.ModifyDate
                }
            };
        }
    }
}
