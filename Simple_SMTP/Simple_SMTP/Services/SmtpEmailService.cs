using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Simple_SMTP.Models;

namespace Simple_SMTP.Services
{

    public interface IEmailService
    {

        #region - - - - - Methods - - - - -

        Task<bool> SendEmailAsync(EmailMessage emailMessage);

        #endregion

    }

    // Note: This is only configured for hardcoded settings. Also service does not singleton scope.
    public class SmtpEmailService : IEmailService
    {

        #region - - - - - Fields - - - - -

        private readonly string m_SmtpServer;
        private readonly SmtpSettings m_Settings;

        #endregion Fields

        #region - - - - - Constructors - - - - -

        public SmtpEmailService(SmtpSettings smtpSettings)
        {
            this.m_SmtpServer = "smtp-mail.outlook.com";
            this.m_Settings = smtpSettings ?? throw new ArgumentNullException(nameof(smtpSettings));
        }

        #endregion Constructors

        #region - - - - - Methods - - - - -

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            try
            {
                using (var _SmtpClient = new SmtpClient(this.m_SmtpServer, this.m_Settings.Port))
                {
                    _SmtpClient.Credentials = new NetworkCredential(this.m_Settings.UserName, this.m_Settings.Password);
                    _SmtpClient.EnableSsl = this.m_Settings.EnableSsl;

                    var _MailMessage = new MailMessage
                    {
                        From = new MailAddress(this.m_Settings.UserName),
                        Subject = emailMessage.Subject,
                        Body = emailMessage.Body,
                        IsBodyHtml = true
                    };
                    _MailMessage.To.Add(emailMessage.To);

                    await _SmtpClient.SendMailAsync(_MailMessage);

                    Debug.WriteLine(">>> Successfully send email.");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        #endregion Methods

    }

}
