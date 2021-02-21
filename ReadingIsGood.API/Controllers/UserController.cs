using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Core.Model.User;
using ReadingIsGood.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        #region Member(s)
        private readonly IUserService userService;
        #endregion

        #region Constructor(s)
        public UserController(
            IUserService userService)
        {
            this.userService = userService;
        }
        #endregion

        [HttpGet("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var token = await this.userService.Login(request).ConfigureAwait(false);

            if (!token.IsSuccess)
            {
                return this.BadRequest(token);
            }

            return Ok(token);
        }
    }
}
