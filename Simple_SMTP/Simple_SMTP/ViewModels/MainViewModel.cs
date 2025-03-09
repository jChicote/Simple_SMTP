using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Simple_SMTP.Commands;
using Simple_SMTP.Models;
using Simple_SMTP.Views;

namespace Simple_SMTP.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {

        #region - - - - - Fields - - - - -

        private string m_LoggedInUserName;

        #endregion Fields

        #region - - - - - Properties - - - - -

        public string LoggedInUserName
        {
            get => this.m_LoggedInUserName;
            set
            {
                this.m_LoggedInUserName = value;
                this.OnPropertyChanged(nameof(LoggedInUserName));
            }
        }

        public ObservableCollection<EmailMessage> SentEmailMessages { get; set; }

        public ICommand NewEmailCommand { get; set; }

        #endregion Properties

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
                //To = "test@email.com"
            });
            SentEmailMessages.Add(new EmailMessage
            {
                SentTime = DateTime.Now,
                Body = "Test Body 2",
                Subject = "Test Message 2",
                //To = "test2@email.com"
            });
            SentEmailMessages.Add(new EmailMessage
            {
                SentTime = DateTime.Now,
                Body = "Test Body 3",
                Subject = "Test Message 3",
                //To = "test3@email.com"
            });

            this.NewEmailCommand = new RelayCommand(this.CreateNewEmail, this.CanCreateEmail);
        }

        #endregion Constructors

        #region - - - - - Events - - - - -

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Events

        #region - - - - - Methods - - - - -

        private bool CanCreateEmail(object obj)
            => true;

        private void CreateNewEmail(object sender)
        {
            var _EditEmailModalWindow = new EditEmailMessageModal();
            _EditEmailModalWindow.Show();
        }

        private void DisplayLoggedInUserName()
        {

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion Methods

    }

}
