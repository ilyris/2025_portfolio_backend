using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PortfolioAPI.Models.Data;


public partial class User
{
    [Key]
    public required Guid UniqueId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

}