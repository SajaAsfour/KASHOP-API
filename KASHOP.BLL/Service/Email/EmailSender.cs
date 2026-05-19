using System.Net;
using System.Net.Mail;

namespace KASHOP.BLL.Service.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sajanazih2004@gmail.com", "dtyo tffl crck rcwp")
            };

            return client.SendMailAsync(
                new MailMessage(from: "sajanazih2004@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                { IsBodyHtml=true}
                );
        }
    }
}
