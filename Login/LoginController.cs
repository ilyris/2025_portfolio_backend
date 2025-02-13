using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> LoginUser(UserLogin user)
        {
            var response = await _loginService.LoginUser(user);
            if (response == null) return BadRequest(new { message = "Email and/or Password is incorrect." });
            return Ok(response);
        }
    }
}
