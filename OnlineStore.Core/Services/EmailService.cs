using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace OnlineStore.Core.Services;

public class EmailService : IEmailService
{

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    private IConfiguration _configuration;



    public async Task SendEmailAsync(string toEmail, string subjectText, string bodyText)
    {
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(
            new MailboxAddress(
                _configuration["EmailSettings:EmailName"],
                _configuration["EmailSettings:Email"] + "@"
                + _configuration["EmailSettings:EmailDomain"]
            )
        );
        emailMessage.To.Add(new MailboxAddress("", toEmail));

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = bodyText;

        emailMessage.Subject = subjectText;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        await SendAsync(emailMessage);
    }

    public async Task SendFileAsync(string toEmail, string subjectText, string bodyText, string fileName)
    {
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(
            new MailboxAddress(
                _configuration["EmailSettings:EmailName"],
                _configuration["EmailSettings:Email"] + "@"
                + _configuration["EmailSettings:EmailDomain"]
            )
        );
        emailMessage.To.Add(new MailboxAddress("", toEmail));

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = bodyText;
        await bodyBuilder.Attachments.AddAsync(fileName);

        emailMessage.Subject = subjectText;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        await SendAsync(emailMessage);
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();

        await client.ConnectAsync(
            _configuration["EmailSettings:Host"],
            int.Parse(_configuration["EmailSettings:Port"]),
            MailKit.Security.SecureSocketOptions.StartTls
        );

        await client.AuthenticateAsync(
            _configuration["EmailSettings:Email"],
            _configuration["EmailSettings:Password"]
        );

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
