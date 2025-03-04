using Amazon.S3;
using Amazon.S3.Model;
using PortfolioAPI.Services;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName = "2025-portfolio-project-images";

    public S3Service(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public string GeneratePresignedUrl(string fileName, string contentType)
    {
        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = $"{fileName}",
                Expires = DateTime.UtcNow.AddMinutes(15),
                Verb = HttpVerb.PUT,
                ContentType = contentType
            };

            var presignedUrl = _s3Client.GetPreSignedURL(request);
            Console.WriteLine("Generated Pre-Signed URL: " + presignedUrl);
            return presignedUrl;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating pre-signed URL", ex);
        }
    }
}
