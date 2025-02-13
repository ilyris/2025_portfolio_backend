using System;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models.Data;

namespace PortfolioAPI.Login;

public class LoginService(AppDbContext dbContext, IPasswordHasher passwordHasher)
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<IActionResult> CreateUser([FromBody] UserSignupDto user)
    {
        Guid newUserGuid = Guid.NewGuid();
        var hashedPassword = _passwordHasher.Hash(user.Password);
        var newUser = new User
        {
            UniqueId = newUserGuid,
            Email = user.Email,
            // Hash the password
            Password = hashedPassword,
        };
        try
        {
            // Add the new user to the Users table and save the changes
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            // Return an OkObjectResult (this is the ActionResult)
            return new OkObjectResult(
                new { message = "User created successfully", userId = newUser.UniqueId }
            );
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during the insert operation
            return new BadRequestObjectResult(
                new { message = "An error occurred", error = ex.Message }
            );
        }
    }
}
