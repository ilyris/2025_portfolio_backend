using Microsoft.AspNetCore.Mvc;

namespace PortfolioAPI.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] EmailRequestDto emailObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var emailMessage = _emailService.SendMessage(emailObject);
            return Ok(emailMessage);
        }
    }
}
