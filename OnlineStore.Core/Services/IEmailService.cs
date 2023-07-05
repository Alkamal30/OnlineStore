namespace OnlineStore.Core.Services;

public interface IEmailService {
    Task SendEmailAsync(string toEmail, string subjectText, string bodyText);
    Task SendFileAsync(string toEmail, string subjectText, string bodyText, string fileName);
}
