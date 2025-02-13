using System;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models.Data;

namespace PortfolioAPI.Login;

public class LoginService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public LoginService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUser([FromBody] User user)
    {

        // var response = await _dbContext.Users
        Guid newUserGuid = Guid.NewGuid();

        var newUser = new User
        {
            UniqueId = newUserGuid,
            Email = user.Email,
            // Hash the password
            Password = user.Password
        };
    }
}
