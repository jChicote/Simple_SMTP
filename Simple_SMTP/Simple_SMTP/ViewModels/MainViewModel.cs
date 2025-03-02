using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Simple_SMTP.Commands;
using Simple_SMTP.Models;

namespace Simple_SMTP.ViewModels
{

    public class MainViewModel
    {
        #region - - - - - Fields - - - - -

        public ObservableCollection<EmailMessage> SentEmailMessages { get; set; }

        public ICommand NewEmailCommand { get; set; }

        #endregion Fields

        #region - - - - - Constructors - - - - -

        public MainViewModel()
        {
            SentEmailMessages = new ObservableCollection<EmailMessage>();

            // Insert some test data
            SentEmailMessages.Add(new EmailMessage
            {
                SentTime = DateTime.Now,
                Body = "Test Body 1",
                Subject = "Test Message 1",
                To = "test@email.com"
            });
            SentEmailMessages.Add(new EmailMessage
            {
                SentTime = DateTime.Now,
                Body = "Test Body 2",
                Subject = "Test Message 2",
                To = "test2@email.com"
            });
            SentEmailMessages.Add(new EmailMessage
            {
                SentTime = DateTime.Now,
                Body = "Test Body 3",
                Subject = "Test Message 3",
                To = "test3@email.com"
            });

            this.NewEmailCommand = new RelayCommand(this.CreateNewEmail, this.CanCreateEmail);
        }

        #endregion Constructors

        #region - - - - - Methods - - - - -

        private bool CanCreateEmail(object obj)
            => true;

        private void CreateNewEmail(object sender)
        {
            Debug.WriteLine("Command is working");
        }

        #endregion Methods

    }

}
