using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Simple_SMTP.Models;

namespace Simple_SMTP.ViewModels
{

    public class MainViewModel
    {
        #region - - - - - Fields - - - - -

        public ObservableCollection<EmailMessage> SentEmailMessages { get; set; }

        public ICommand ShowWindowCommand { get; set; }

        #endregion Fields

        #region - - - - - Constructors - - - - -

        public MainViewModel()
        {
            SentEmailMessages = new ObservableCollection<EmailMessage>();

        }

        #endregion Constructors
    }

}
