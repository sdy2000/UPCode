using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Core.Senders
{
    public class SendEmail
    {
        public static bool Send(string To, string Subject, string Body)
        {
            try
            {
                MimeMessage mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("gmail@gmail.com"));
                mail.To.Add(MailboxAddress.Parse(To));
                mail.Subject = Subject;
                mail.Body = new TextPart(TextFormat.Html) { Text = Body };

                using SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Connect("smtp.gmail.com", 587,SecureSocketOptions.StartTls);
                SmtpServer.Authenticate("gmail@gmail.com", "********");
                SmtpServer.Send(mail);
                SmtpServer.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}