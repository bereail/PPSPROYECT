using MiniMarket_API.Application.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var fromMail = "noreply.minimarket@gmail.com";
            var appPwrd = "bmiy pdcs wnwc jmgc";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, appPwrd)
            };

            return client.SendMailAsync(
                new MailMessage(from: fromMail,
                to: email,
                subject,
                message));
        }
    }
}
