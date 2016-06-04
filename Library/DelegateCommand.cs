using System;
using System.Windows.Input;

namespace Library {
    public class DelegateCommand : ICommand {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            if (_canExecute == null) {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }

        public DelegateCommand(Action<object> execute) : this(execute, null) {

        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
