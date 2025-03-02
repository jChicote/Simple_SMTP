using System.Windows;
using Simple_SMTP.ViewModels;

namespace Simple_SMTP.Views
{

    /// <summary>
    /// Interaction logic for EditEmailMessageModal.xaml
    /// </summary>
    public partial class EditEmailMessageModal : Window
    {

        #region - - - - - Constructors - - - - -

        public EditEmailMessageModal()
        {
            InitializeComponent();

            EditEmailMessageViewModel _ViewModel = new();
            this.DataContext = _ViewModel;
        }

        #endregion Constructors

    }

}
