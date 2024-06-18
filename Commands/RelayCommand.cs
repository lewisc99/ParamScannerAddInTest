using System;
using System.Windows.Input;

namespace ParamScannerAddIn.Commands
{
    public class RelayCommand : ICommand
    {
        #region Delegates
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute; 
        #endregion

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #region Constructor for the RelayCommand class
        /// <summary>
        /// Constructor for the RelayCommand class.
        /// </summary>
        /// <param name="execute">The action to be executed when the command is invoked. This parameter cannot be null.</param>
        /// <param name="canExecute">The function that determines whether the command can be executed. This parameter is optional and defaults to null, meaning the command can always be executed.</param>
        /// <exception cref="ArgumentNullException">Thrown if the 'execute' parameter is null.</exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        #endregion

        #region To Execute Commands and MVVM methods
        /// <summary>
        /// To Execute Commands and MVVM methods
        /// </summary>
        /// <param name="parameter">Could be a method or to trigger</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        } 
        #endregion
    }
}
