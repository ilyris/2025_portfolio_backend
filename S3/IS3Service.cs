namespace PortfolioAPI.Services
{
    public interface IS3Service
    {
        string GeneratePresignedUrl(string fileName, string contentType);
    }
}
