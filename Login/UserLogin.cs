using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Login;

public partial class UserLogin
{
    public required string Email { get; set; }

    public required string Password { get; set; }

}