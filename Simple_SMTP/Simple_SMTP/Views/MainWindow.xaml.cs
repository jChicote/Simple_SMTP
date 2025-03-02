using Simple_SMTP.ViewModels;
using System.Windows;

namespace Simple_SMTP.Views
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region - - - - - Constructors - - - - -

        public MainWindow()
        {
            InitializeComponent();

            // Sets the ViewModel to be databinded to the Window's datacontext
            MainViewModel _MainViewModel = new();
            this.DataContext = _MainViewModel;
        }

        #endregion Constructors

    }

}
