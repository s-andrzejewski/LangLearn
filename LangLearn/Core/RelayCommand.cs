using System;
using System.Windows.Input;

namespace LangLearn.Core
{
    class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        // invoked when the possibility of command invoke state changes
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // RelayCommand object constructor
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // function 1
        public bool CanExecute(object parameter)
        {
            // when the parameter is set then invoked with parameter (after ||), or whether not then returns null
            return _canExecute == null || _canExecute(parameter);
        }

        // function 2
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
