namespace PortfolioAPI.Email;

public class EmailRequestDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Message { get; set; }
}
