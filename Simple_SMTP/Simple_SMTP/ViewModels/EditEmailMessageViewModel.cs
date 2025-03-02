using System.Diagnostics;
using System.Windows.Input;
using Simple_SMTP.Commands;
using Simple_SMTP.Models;
using Simple_SMTP.Services;

namespace Simple_SMTP.ViewModels
{

    public class EditEmailMessageViewModel
    {

        #region - - - - - Fields - - - - -

        private readonly IEmailService m_EmailService;

        #endregion Fields

        #region - - - - - Properties - - - - -

        public EmailMessage Message { get; set; }

        public ICommand SendEmailCommand { get; set; }

        #endregion Properties

        #region - - - - - Constructors - - - - -

        public EditEmailMessageViewModel()
        {
            this.m_EmailService = new SmtpEmailService(new SmtpSettings
            {
                Port = 587,
                EnableSsl = true,
                UserName = "",
                Password = ""
            });

            this.Message = new();
            this.SendEmailCommand = new RelayCommand(async (object obj) => this.SendEmail(obj), this.CanSendEmail);
        }

        #endregion Constructors

        #region - - - - - Methods - - - - -

        private bool CanSendEmail(object obj)
            => true;

        private async Task SendEmail(object obj)
        {
            bool _IsSuccessful = await this.m_EmailService.SendEmailAsync(this.Message);

            if (_IsSuccessful)
                Debug.WriteLine(">>> Successfully send email.");
            else
                Debug.WriteLine(">>> Failed sending email.");
        }

        #endregion Methods

    }

}
