namespace SkyCES.EntLib
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public sealed class MailClass
    {
        public static void SendEmail(MailInfo mail)
        {
            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.Default;
            message.From = new MailAddress(mail.UserName);
            try
            {
                message.To.Add(mail.ToEmail);
            }
            catch
            {
            }
            message.Subject = mail.Title;
            message.Body = mail.Content;
            message.IsBodyHtml = mail.IsBodyHtml;
            try
            {
                SmtpClient client = new SmtpClient(mail.Server);
                client.Port = mail.ServerPort;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mail.UserName, mail.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
            }
            catch
            {
                throw new Exception("邮件配置错误，请检查");
            }
        }
    }
}

