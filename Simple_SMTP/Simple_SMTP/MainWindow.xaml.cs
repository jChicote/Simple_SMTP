using System.Windows;
using Simple_SMTP.ViewModels;

namespace Simple_SMTP
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Sets the ViewModel to be databinded to the Window's datacontext
            MainViewModel _MainViewModel = new();
            this.DataContext = _MainViewModel;
        }
    }

}