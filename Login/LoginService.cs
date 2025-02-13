using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PortfolioAPI.Login;

public class LoginService(AppDbContext dbContext, IPasswordHasher passwordHasher)
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<UserLoginResponseDto?> LoginUser([FromBody] UserLogin user)
    {
        var userEntity = await _dbContext.Users
            .Where(u => u.Email == user.Email)
            .FirstOrDefaultAsync();

        var verifyUserPassword = _passwordHasher.Verify(userEntity.Password, user.Password);

        if (!verifyUserPassword || userEntity == null) return null;
        var userResponse = new UserLoginResponseDto
        {
            Email = userEntity.Email,
            UniqueId = userEntity.UniqueId
        };
        return userResponse;
    }
}
