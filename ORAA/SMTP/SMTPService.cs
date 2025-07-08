using System.Net;
using System.Net.Mail;

namespace ORAA.SMTP
{
    public class SMTPService
    {
        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
            try
            {
                string senderEmail = "khulelidzeliza@gmail.com";
                string appPassword = "zeil smiy tjgb htjv";

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(toAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(senderEmail, appPassword);
                        smtpClient.Timeout = 30000; // 30 seconds timeout

                        await smtpClient.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Re-throw if you want calling code to handle it
            }
        }
        public void SendEmail(string toAddress, string subject, string body)
        {
            string senderEmail = "k.maminaishvili@gmail.com";
            string appPassword = "ykws duyg kznm rhiu";


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword)
            };

            smtpClient.Send(mail);

        }
    }
}
