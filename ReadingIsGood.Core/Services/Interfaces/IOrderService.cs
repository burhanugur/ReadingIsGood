using ReadingIsGood.Core.Model;
using ReadingIsGood.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<Guid>> CreateOrder(OrderCreateRequest request);

        Task<ServiceResponse<OrderDto>> UpdateOrderStatus(Guid id);

        Task<ServiceResponse<List<OrderDto>>> GetOrderList(Guid customerId);

        Task<ServiceResponse<OrderDto>> GetOrder(Guid id);
    }
}
