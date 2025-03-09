using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Identity.Client;
using MimeKit;
using Simple_SMTP.Models;

namespace Simple_SMTP.Services
{

    public class MailKitEmailService : IEmailService
    {

        #region - - - - - Fields - - - - -

        private readonly SmtpSettings m_Settings;

        #endregion Fields

        #region - - - - - Constructors - - - - -

        public MailKitEmailService()
        {
            this.m_Settings = new SmtpSettings()
            {
                AuthenticationMethod = "OAuth2",
                Encryption = SecureSocketOptions.StartTls,
                Port = 587,
                ServerAddress = "smtp-mail.outlook.com",
                EnableSsl = true,
                UserName = "",
                Password = ""
            };
        }

        #endregion Constructors

        #region - - - - - Methods - - - - -

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            var _Message = new MimeMessage();

            _Message.From.Add(new MailboxAddress(emailMessage.From.Address, emailMessage.From.Name));
            _Message.Subject = emailMessage.Subject;
            _Message.Body = new TextPart("plain")
            {
                Text = emailMessage.Body
            };

            foreach (var _Recipient in emailMessage.To)
                _Message.To.Add(new MailboxAddress(_Recipient.Name, _Recipient.Address));

            var _Result = await GetPublicClientOAuth2CredentialsAsync("SMTP", emailMessage.From.Address);
            var _OAuth2 = new SaslMechanismOAuth2(_Result.Account.Username, _Result.AccessToken);
            using (var _Client = new SmtpClient())
            {
                _Client.Connect(this.m_Settings.ServerAddress, this.m_Settings.Port, this.m_Settings.Encryption);
                _Client.Authenticate(_OAuth2);
                _Client.Send(_Message);
                _Client.Disconnect(true);
            }

            return true;
        }

        // Taken from: https://github.com/jstedfast/MailKit/blob/master/ExchangeOAuth2.md
        // Application ID and Directory ID need to be retrieved from outlook to continue OAuth token flow
        static async Task<AuthenticationResult> GetPublicClientOAuth2CredentialsAsync(
            string protocol,
            string emailAddress,
            CancellationToken cancellationToken = default)
        {
            var options = new PublicClientApplicationOptions
            {
                ClientId = "Application (client) ID",
                TenantId = "Directory (tenant) ID",

                // Use "https://login.microsoftonline.com/common/oauth2/nativeclient" for apps using
                // embedded browsers or "http://localhost" for apps that use system browsers.
                RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"
            };

            var publicClientApplication = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(options)
                .Build();

            string[] scopes;

            if (protocol.Equals("IMAP", StringComparison.OrdinalIgnoreCase))
            {
                scopes = new string[] {
            "email",
            "offline_access",
            "https://outlook.office.com/IMAP.AccessAsUser.All"
        };
            }
            else if (protocol.Equals("POP", StringComparison.OrdinalIgnoreCase))
            {
                scopes = new string[] {
            "email",
            "offline_access",
            "https://outlook.office.com/POP.AccessAsUser.All"
        };
            }
            else
            {
                scopes = new string[] {
            "email",
            "offline_access",
            "https://outlook.office.com/SMTP.Send"
        };
            }

            try
            {
                // First, check the cache for an auth token.
                return await publicClientApplication.AcquireTokenSilent(scopes, emailAddress).ExecuteAsync(cancellationToken);
            }
            catch (MsalUiRequiredException)
            {
                // If that fails, then try getting an auth token interactively.
                return await publicClientApplication.AcquireTokenInteractive(scopes).WithLoginHint(emailAddress).ExecuteAsync(cancellationToken);
            }
        }

        #endregion Methods

    }

}
