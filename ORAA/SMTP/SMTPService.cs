using System.Net;
using System.Net.Mail;

namespace ORAA.SMTP
{
    public class SMTPService
    {
        public async Task SendEmailAsync(string toAddress, string subject)
        {
            try
            {
                string senderEmail = "khulelidzeliza@gmail.com";
                string appPassword = "zeil smiy tjgb htjv";
                string BodyHTML = "<h1>Welcome to ORAA</h1><p>Thank you for signing up!</p>";
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(toAddress);
                    mail.Subject = subject;
                    mail.Body = BodyHTML;
                    mail.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(senderEmail, appPassword);
                        smtpClient.Timeout = 30000; // 30 seconds timeout  // IDK WHAT IS THIS AND IF NEEDED

                        await smtpClient.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
    }
}
