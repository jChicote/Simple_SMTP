using System.Windows.Input;

namespace Simple_SMTP.Commands
{

    public class RelayCommand : ICommand
    {

        #region - - - - - Fields - - - - -

        private Action<object> ExecuteAction { get; set; }

        private Predicate<object> CanExecutePredicate { get; set; }

        #endregion

        #region - - - - - Constructors - - - - -

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecutePredicate)
        {
            this.ExecuteAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.CanExecutePredicate =
                canExecutePredicate ?? throw new ArgumentNullException(nameof(canExecutePredicate));
        }

        # endregion Constructors

        #region - - - - - Events - - - - -

        public event EventHandler? CanExecuteChanged;

        #endregion Events

        #region - - - - - Methods - - - - -

        public bool CanExecute(object? parameter)
            => this.CanExecutePredicate.Invoke(parameter);

        public void Execute(object? parameter)
            => this.ExecuteAction.Invoke(parameter);

        #endregion Methods

    }

}