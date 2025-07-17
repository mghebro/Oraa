using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ORAA.SMTP
{
    public class SMTPService
    {
        private const string SenderEmail = "khulelidzeliza@gmail.com";
        private const string AppPassword = "zeil smiy tjgb htjv";

        public static string GenerateVerificationCode()
        {
            // Use cryptographically secure random number generator
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            var randomNumber = BitConverter.ToUInt32(bytes, 0);
            return (randomNumber % 900000 + 100000).ToString();
        }

        public static async Task<string> SendVerificationCodeAsync(string toEmail, string userName)
        {
            string verificationCode = GenerateVerificationCode();

            string htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1.0' />
  <title>Your Verification Code</title>
</head>
<body style='margin:0; padding:0; background-color:#f0f0f0;'>
  <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0' style='padding: 40px 20px; font-family: -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, sans-serif;'>
    <tr>
      <td align='center'>
        <table role='presentation' cellpadding='0' cellspacing='0' border='0' style='max-width: 600px; background-color: #e8e8e8; border-radius: 30px; padding: 40px 20px;'>
          <tr>
            <td align='center' style='padding-bottom: 20px;'>
              <h1 style='font-size: 26px; font-weight: 600; color: #333; margin: 0 0 20px;'>Here's Your Verification Code</h1>
              <p style='font-size: 15px; color: #666; line-height: 1.5; margin: 0 0 30px;'>To continue your journey with <strong>Ora</strong>, enter the code below to verify your identity.</p>
            </td>
          </tr>

          <!-- Code Digits -->
        <tr>
  <td align='center' style='padding-bottom: 30px;'>
    <table role='presentation' cellpadding='0' cellspacing='8' border='0'>
      <tr>
        <td style='background-color: #d0d0d0; width: 40px; height: 40px; border-radius: 8px; text-align: center; vertical-align: middle; font-size: 18px; font-weight: bold; color: #333; line-height: 40px;'>{verificationCode[0]}</td>
        <td style='background-color: #d0d0d0; width: 40px; height: 40px; border-radius: 8px; text-align: center; vertical-align: middle; font-size: 18px; font-weight: bold; color: #333; line-height: 40px;'>{verificationCode[1]}</td>
        <td style='background-color: #d0d0d0; width: 40px; height: 40px; border-radius: 8px; text-align: center; vertical-align: middle; font-size: 18px; font-weight: bold; color: #333; line-height: 40px;'>{verificationCode[2]}</td>
        <td style='background-color: #d0d0d0; width: 40px; height: 40px; border-radius: 8px; text-align: center; vertical-align: middle; font-size: 18px; font-weight: bold; color: #333; line-height: 40px;'>{verificationCode[3]}</td>
        <td style='background-color: #d0d0d0; width: 40px; height: 40px; border-radius: 8px; text-align: center; vertical-align: middle; font-size: 18px; font-weight: bold; color: #333; line-height: 40px;'>{verificationCode[4]}</td>
        <td style='background-color: #d0d0d0; width: 40px; height: 40px; border-radius: 8px; text-align: center; vertical-align: middle; font-size: 18px; font-weight: bold; color: #333; line-height: 40px;'>{verificationCode[5]}</td>
      </tr>
    </table>
  </td>
</tr>


          <!-- Footer Message -->
          <tr>
            <td align='center'>
              <p style='font-size: 14px; color: #666; margin: 0 0 30px;'>If you didn't request this code, feel free to ignore this email.</p>
              <p style='font-size: 14px; color: #333; font-weight: 500; margin: 0;'>With purpose and protection,<br/>The Ora Team (Magnora & Zenora)</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>
";


            using var mail = new MailMessage();
            mail.From = new MailAddress(SenderEmail, "ORA");
            mail.To.Add(toEmail);
            mail.Subject = "Your Verification Code - ORA";
            mail.Body = htmlContent;
            mail.IsBodyHtml = true;

            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(SenderEmail, AppPassword),
            };

            await smtpClient.SendMailAsync(mail);
            return verificationCode;
        }
        public void SendEmail(string to, string subject, string body)
        {
            string verificationCode = GenerateVerificationCode();

            string htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Your Verification Code</title>
</head>
<body style='font-family: -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, sans-serif; margin: 0; padding: 40px 20px; background-color: #f0f0f0;'>
    <div style='max-width: 600px; margin: 0 auto; background-color: #e8e8e8; border-radius: 30px; padding: 50px 40px; text-align: center;'>
        
        <h1 style='font-size: 32px; font-weight: 600; color: #333; margin: 0 0 30px; letter-spacing: -0.5px;'>Here's Your Verification Code</h1>
        
        <p style='font-size: 16px; color: #666; line-height: 1.6; margin: 0 0 40px; max-width: 500px; margin-left: auto; margin-right: auto;'>
            To continue your journey with <strong>Ora</strong>, please enter the code below to verify your identity. This helps us keep your experience secure and personalized.
        </p>
        
        <div style='margin: 50px 0;'>
            <table style='margin: 0 auto; border-collapse: separate; border-spacing: 8px;'>
                <tr>
                    <td style='background-color: #d0d0d0; width: 60px; height: 60px; border-radius: 15px; text-align: center; font-size: 28px; font-weight: 600; color: #333; vertical-align: middle;'>{verificationCode[0]}</td>
                    <td style='background-color: #d0d0d0; width: 60px; height: 60px; border-radius: 15px; text-align: center; font-size: 28px; font-weight: 600; color: #333; vertical-align: middle;'>{verificationCode[1]}</td>
                    <td style='background-color: #d0d0d0; width: 60px; height: 60px; border-radius: 15px; text-align: center; font-size: 28px; font-weight: 600; color: #333; vertical-align: middle;'>{verificationCode[2]}</td>
                    <td style='background-color: #d0d0d0; width: 60px; height: 60px; border-radius: 15px; text-align: center; font-size: 28px; font-weight: 600; color: #333; vertical-align: middle;'>{verificationCode[3]}</td>
                    <td style='background-color: #d0d0d0; width: 60px; height: 60px; border-radius: 15px; text-align: center; font-size: 28px; font-weight: 600; color: #333; vertical-align: middle;'>{verificationCode[4]}</td>
                    <td style='background-color: #d0d0d0; width: 60px; height: 60px; border-radius: 15px; text-align: center; font-size: 28px; font-weight: 600; color: #333; vertical-align: middle;'>{verificationCode[5]}</td>
                </tr>
            </table>
        </div>
        
        <p style='font-size: 16px; color: #666; line-height: 1.6; margin: 40px 0;'>
            If you didn't request this code, you can safely ignore this message.
        </p>
        
        <div style='margin-top: 60px;'>
            <p style='font-size: 16px; color: #333; font-weight: 500; margin: 0;'>With purpose and protection,</p>
            <p style='font-size: 16px; color: #333; font-weight: 500; margin: 5px 0 0;'>The Ora Team (Magnora & Zenora)</p>
        </div>
        
    </div>
</body>
</html>
";

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
