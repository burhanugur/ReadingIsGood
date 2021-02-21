using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Core.Model.Customer;
using ReadingIsGood.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [Route("customer")]
    public class CustomerController : BaseController
    {
        #region Member(s)
        private readonly ICustomerService customerService;
        #endregion

        #region Constructor(s)
        public CustomerController(
            ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        #endregion

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCustomer(CustomerCreateRequest request)
        {
            var result = await this.customerService.CreateCustomer(request);

            if (!result.IsSuccess)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var customerId = identity.Claims.FirstOrDefault(x => x.Type == "CustomerId").Value;

            var info = await this.customerService.GetCustomerById(new Guid(customerId));

            if (!info.IsSuccess)
            {
                return this.BadRequest(info);
            }

            return this.Ok(info);
        }
    }
}
