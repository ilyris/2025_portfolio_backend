

namespace PortfolioAPI.Login;

public partial class UserLoginResponseDto
{
    public required string Email { get; set; }

    public required Guid UniqueId { get; set; }

}