using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI;
using PortfolioAPI.Models.Data;

namespace PortfolioAPI.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> SignupUser(User user)
        {
            var response = await _loginService.CreateUser(user)

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
