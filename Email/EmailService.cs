
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PortfolioAPI.Email;

public class EmailService
{
    private readonly SmtpClient _smtpClient;
    public string emailUsername;

    public EmailService(IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        _smtpClient = new SmtpClient(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]));
        _smtpClient.Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["AppPassword"]);
        _smtpClient.EnableSsl = true;
        emailUsername = emailSettings["Username"]!;

    }
    public object SendMessage(EmailRequestDto emailObject)
    {
        // Create email message
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(emailUsername);
        mailMessage.To.Add(emailUsername);
        mailMessage.Subject = emailObject.Name + " - General information regarding Dylan Sieren";
        mailMessage.IsBodyHtml = true;
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat($"<h1>{emailObject.Name} General Information</h1>");
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat($"<p>{emailObject.Message}</p>");
        mailBody.AppendFormat($"<p>From Email: {emailObject.Email}</p>");
        mailMessage.Body = mailBody.ToString();

        // Send email
        _smtpClient.Send(mailMessage);

        return new { message = "Email Successfully Sent", messageSender = emailObject.Email };

    }
}
