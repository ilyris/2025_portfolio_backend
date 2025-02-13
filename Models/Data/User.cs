using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models.Data;

public partial class User
{
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    [Column("uniqueid")]
    public required Guid UniqueId { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("password")]
    public required string Password { get; set; }

}