using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace ECommerceWeb.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Obtener configuración de Gmail
            string userName = _configuration.GetValue<string>("Email:UserName");
            string password = _configuration.GetValue<string>("Email:PassWord");

            var correoAEnviar = new MimeMessage();
            correoAEnviar.From.Add(MailboxAddress.Parse(userName));
            correoAEnviar.To.Add(MailboxAddress.Parse(email));
            correoAEnviar.Subject = subject;
            correoAEnviar.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using (var clienteSmtp = new SmtpClient())
            {
                clienteSmtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                clienteSmtp.Authenticate(userName, password);
                clienteSmtp.Send(correoAEnviar);
                clienteSmtp.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
