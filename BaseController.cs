using System;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioAPI;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BaseController : ControllerBase
{

}
