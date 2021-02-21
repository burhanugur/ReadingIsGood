using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Core.Model.Order;
using ReadingIsGood.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [Route("order")]
    public class OrderController : BaseController
    {
        #region Member(s)
        private readonly IOrderService orderService;
        #endregion

        #region Constructor(s)
        public OrderController(
            IOrderService orderService)
        {
            this.orderService = orderService;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateRequest request)
        {
            var result = await this.orderService.CreateOrder(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return this.Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId)
        {
            var result = await this.orderService.UpdateOrderStatus(orderId);

            if (!result.IsSuccess)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var customerId = identity.Claims.FirstOrDefault(x => x.Type == "CustomerId").Value;

            var result = await this.orderService.GetOrderList(new Guid(customerId));

            return this.Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await this.orderService.GetOrder(orderId);

            if (!order.IsSuccess)
            {
                return this.BadRequest(order);
            }

            return Ok(order);
        }
    }
}
