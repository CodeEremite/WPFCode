using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfExtended.MVVM
{
    public class RelayCommandGeneric<T> : ICommand
    {
        private readonly Action<T>    _executeMethod    = null;
        private readonly Func<T,bool> _canExecuteMethod = null;
        private bool                  _isAutomaticRequeryDisabled = false;

        public RelayCommandGeneric(Action<T> executeMethod, Func<T,bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            if(executeMethod ==null)
            {
                throw new ArgumentNullException();
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
                    CommandManager.RequerySuggested += value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if(_canExecuteMethod != null && parameter is T)
            {
                _canExecuteMethod((T)parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if(_executeMethod != null && parameter is T)
            {
                _executeMethod((T)parameter);                   
            }
        }
    }
}
