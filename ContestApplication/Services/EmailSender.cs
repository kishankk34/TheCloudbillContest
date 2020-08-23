using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
//using SendGrid;
//using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ContestApplication.Services
{
    public class EmailSender : IEmailSender
    {
        //private string htmlString;

        //public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        //{
        //    //Options = optionsAccessor.Value;
        //}

        //public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message1)
        {
            //return Execute("qB!68.cgAHND", subject, message, email);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("notification@thecloudbill.com");
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = message1;
            smtp.Port = 587;
            smtp.Host = "mail.thecloudbill.com"; //for gmail host  
                                                 //smtp.Host = "thecloudbill.com"; //for gmail host  
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("notification@thecloudbill.com", "M,*B5V1#ZGK[");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            return smtp.SendMailAsync(message);
        }

        //public Task Execute(string apiKey, string subject, string message1, string email)
        //{
        //    var client = new SendGridClient(apiKey);
        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress("Joe@contoso.com", Options.SendGridUser),
        //        Subject = subject,
        //        PlainTextContent = message,
        //        HtmlContent = message
        //    };
        //    msg.AddTo(new EmailAddress(email));

        //    // Disable click tracking.
        //    // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        //    msg.SetClickTracking(false, false);

        //    return client.SendEmailAsync(msg);

        //}
    }
}