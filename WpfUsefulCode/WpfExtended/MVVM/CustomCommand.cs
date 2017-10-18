using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; 

namespace WpfExtended.MVVM
{
    public class RelayCommand:ICommand
    {
        private readonly Action     _executeMethod = null;
        private readonly Func<bool> _canExecuteMethod = null;
        private bool                _isAutomaticRequeryDisabled = false;

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            if(executeMethod ==null)
            {
                throw new ArgumentNullException("ExecuteMethod is not set.");
            }
            _executeMethod              = executeMethod;
            _canExecuteMethod           = canExecuteMethod;
            _isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if(!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if(!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if(_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _executeMethod?.Invoke();
        }
    }
}
