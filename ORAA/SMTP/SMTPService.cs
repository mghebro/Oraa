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
        public void SendEmail(string to, string subject, string body)
        {
            string senderEmail = "khulelidzeliza@gmail.com";
            string appPassword = "zeil smiy tjgb htjv";

            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress(senderEmail);
                email.To.Add(to);
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(senderEmail, appPassword)
                };

                smtpClient.Send(email);
                Console.WriteLine("Email sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
        public string GetVerificationEmailHtml(string code)
        {
            return @"<!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Verification Code</title>
                <style>
                    body {
                        font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
                        line-height: 1.5;
                        color: #333;
                        margin: 0;
                        padding: 0;
                        background-color: #f7f9fc;
                    }
                    .email-container {
                        max-width: 450px;
                        margin: 0 auto;
                        padding: 25px;
                        background-color: #ffffff;
                        border-radius: 12px;
                        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.07);
                    }
                    .header {
                        text-align: center;
                        padding: 15px 0 20px;
                        border-bottom: 2px solid #6a93ff;
                        margin-bottom: 20px;
                    }
                    .header h2 {
                        color: #4169e1;
                        margin: 0;
                        font-size: 24px;
                        font-weight: 600;
                    }
                    .content {
                        padding: 20px 15px;
                        text-align: center;
                        background-color: #fafbff;
                        border-radius: 8px;
                    }
                    .verification-code {
                        font-size: 28px;
                        font-weight: bold;
                        color: #4169e1;
                        letter-spacing: 3px;
                        padding: 15px 20px;
                        background-color: #ffffff;
                        border-radius: 10px;
                        display: inline-block;
                        margin: 15px 0;
                        box-shadow: 0 3px 10px rgba(65, 105, 225, 0.1);
                        border: 1px dashed #6a93ff;
                    }
                    .message {
                        color: #555;
                        font-size: 16px;
                        margin-bottom: 12px;
                    }
                    .footer {
                        font-size: 13px;
                        text-align: center;
                        color: #888;
                        padding-top: 20px;
                        margin-top: 20px;
                        border-top: 1px solid #eee;
                    }
                    .accent {
                        color: #4169e1;
                    }
                </style>
            </head>
            <body>
                <div class=""email-container"">
                    <div class=""header"">
                        <h2>Account Verification</h2>
                    </div>
                    <div class=""content"">
                        <p class=""message"">Thank you for registering! Please use the following code to verify your account:</p>
                        <div class=""verification-code"">" + code + @"</div>
                        <p class=""message""><span class=""accent"">Important:</span> If you didn't request this code, please ignore this email.</p>
                    </div>
                    <div class=""footer"">
                        <p>This is an automated message, please do not reply.</p>
                        <p>&copy; " + DateTime.Now.Year + @" Your Company Name</p>
                    </div>
                </div>
            </body>
            </html>";
        }

        public string getSuccesed()
        {
            return @"<!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Success Notification</title>
            <style>
                body {
                    font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
                    line-height: 1.5;
                    color: #333;
                    margin: 0;
                    padding: 0;
                    background-color: #f7f9fc;
                }
                .email-container {
                    max-width: 450px;
                    margin: 0 auto;
                    padding: 25px;
                    background-color: #ffffff;
                    border-radius: 12px;
                    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.07);
                }
                .header {
                    text-align: center;
                    padding: 15px 0 20px;
                    border-bottom: 2px solid #4caf50;
                    margin-bottom: 20px;
                }
                .header h2 {
                    color: #2e7d32;
                    margin: 0;
                    font-size: 24px;
                    font-weight: 600;
                }
                .content {
                    padding: 20px 15px;
                    text-align: center;
                    background-color: #f8fff8;
                    border-radius: 8px;
                }
                .success-icon {
                    font-size: 48px;
                    color: #4caf50;
                    margin-bottom: 15px;
                }
                .success-message {
                    font-size: 18px;
                    font-weight: bold;
                    color: #2e7d32;
                    margin-bottom: 15px;
                }
                .reference-code {
                    font-size: 20px;
                    font-weight: bold;
                    color: #2e7d32;
                    letter-spacing: 2px;
                    padding: 10px 15px;
                    background-color: #ffffff;
                    border-radius: 8px;
                    display: inline-block;
                    margin: 10px 0;
                    box-shadow: 0 3px 10px rgba(46, 125, 50, 0.1);
                    border: 1px dashed #4caf50;
                }
                .message {
                    color: #555;
                    font-size: 16px;
                    margin-bottom: 12px;
                }
                .footer {
                    font-size: 13px;
                    text-align: center;
                    color: #888;
                    padding-top: 20px;
                    margin-top: 20px;
                    border-top: 1px solid #eee;
                }
                .accent {
                    color: #2e7d32;
                }
                .button {
                    display: inline-block;
                    background-color: #4caf50;
                    color: white;
                    text-decoration: none;
                    padding: 10px 20px;
                    border-radius: 5px;
                    font-weight: bold;
                    margin-top: 15px;
                }
            </style>
        </head>
        <body>
            <div class=""email-container"">
                <div class=""header"">
                    <h2>Success Confirmation</h2>
                </div>
                <div class=""content"">
                    <div class=""success-icon"">✓</div>
                    <p class=""success-message"">Your operation was completed successfully!</p>                 
                    <p class=""message"">Please keep this code for your records. You may need it for future reference.</p>
                    <p class=""message""><span class=""accent"">Thank you</span> for using our service!</p>
                    <a href=""#"" class=""button"">Go to Dashboard</a>
                </div>
                <div class=""footer"">
                    <p>This is an automated message, please do not reply.</p>
                    <p>&copy; " + DateTime.Now.Year + @" Your Company Name</p>
                </div>
            </div>
        </body>
        </html>";
        }
        public string getNotification()
        {
            return @"
          
    // Read the email template from file or embed it directly in your code
    string emailTemplate = @""<!DOCTYPE html>
<html lang=""""en"""">
<head>
    <meta charset=""""UTF-8"""">
    <meta name=""""viewport"""" content=""""width=device-width, initial-scale=1.0"""">
    <title>Emergency Notification</title>
    <style>
        /* Base styles */
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            line-height: 1.6;
            color: #333333;
            background-color: #f5f5f5;
        }
        
        /* Container */
        .email-container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
        }
        
        /* Header */
        .header {
            background-color: #d9534f;
            padding: 20px;
            text-align: center;
        }
        
        .header h1 {
            color: #ffffff;
            margin: 0;
            font-size: 24px;
        }
        
        /* Alert icon */
        .alert-icon {
            font-size: 40px;
            color: #ffffff;
            margin-bottom: 10px;
        }
        
        /* Content */
        .content {
            padding: 30px 20px;
            background-color: #ffffff;
        }
        
        /* Message */
        .message-container {
            border-left: 4px solid #d9534f;
            padding: 15px;
            background-color: #fdf7f7;
            margin-bottom: 20px;
        }
        
        .message {
            font-size: 16px;
            color: #333333;
            margin: 0;
        }
        
        /* Information */
        .info-container {
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 4px;
        }
        
        .info-label {
            font-weight: bold;
            margin-bottom: 5px;
            color: #555555;
        }
        
        .info-value {
            margin-top: 0;
            margin-bottom: 15px;
        }
        
        /* Actions */
        .actions {
            margin-top: 25px;
            text-align: center;
        }
        
        .button {
            display: inline-block;
            padding: 12px 24px;
            background-color: #0275d8;
            color: #ffffff;
            text-decoration: none;
            border-radius: 4px;
            font-weight: bold;
        }
        
        /* Footer */
        .footer {
            padding: 20px;
            text-align: center;
            font-size: 12px;
            color: #777777;
            background-color: #f5f5f5;
        }
        
        /* Responsive */
        @media only screen and (max-width: 480px) {
            .header h1 {
                font-size: 20px;
            }
            
            .content {
                padding: 20px 15px;
            }
            
            .button {
                display: block;
                margin: 0 auto;
                max-width: 200px;
            }
        }
    </style>
</head>
<body>
    <div class=""""email-container"""">
        <div class=""""header"""">
            <div class=""""alert-icon"""">⚠️</div>
            <h1>EMERGENCY NOTIFICATION</h1>
        </div>
        
        <div class=""""content"""">
            <p>Dear User,</p>
            
            <div class=""""message-container"""">
                <p class=""""message"""">{0}</p>
            </div>
            
            <div class=""""info-container"""">
                <p class=""""info-label"""">Date & Time:</p>
                <p class=""""info-value"""">{1}</p>
                
                <p class=""""info-label"""">Notification ID:</p>
                <p class=""""info-value"""">{2}</p>
            </div>
            
            <div class=""""actions"""">
                <a href=""""{3}"""" class=""""button"""">View Details</a>
            </div>
            
            <p style=""""margin-top: 25px;"""">Please take any necessary precautions and follow instructions from local authorities.</p>
            
            <p>Stay safe,<br>
            Emergency Response Team</p>
        </div>
        
        <div class=""""footer"""">
            <p>This is an automated emergency notification. Please do not reply to this email.</p>
            <p>If you need immediate assistance, please contact your local emergency services.</p>
            <p>&copy; 2025 Emergency Response System. All rights reserved.</p>
        </div>
    </div>
</body>
</html>"";

    // Format the current date and time
    string currentDateTime = DateTime.UtcNow.ToString(""MMMM dd, yyyy 'at' HH:mm 'UTC'"");
    
    // Replace placeholders in the template
    string notificationIdDisplay = notificationId ?? ""N/A"";
    string actionUrl = notificationId != null 
        ? $""https://yourdomain.com/notifications/{notificationId}"" 
        : ""#"";
    
    // Format the HTML with the message and other details
    string formattedHtml = string.Format(
        emailTemplate, 
        message,                 // {0} - Emergency message
        currentDateTime,         // {1} - Date and time
        notificationIdDisplay,   // {2} - Notification ID
        actionUrl                // {3} - Action URL
    );
    
    return formattedHtml;
}
        ";

        }
    }
}
