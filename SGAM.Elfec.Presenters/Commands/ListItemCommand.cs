using System;
using System.Windows.Input;

namespace SGAM.Elfec.Commands
{
    public class ListItemCommand<T> : ICommand
    {
        public delegate void ExecuteDelegate(T item);
        public delegate bool CanExecuteDelegate(T item);
        private ExecuteDelegate _executeDel;
        private CanExecuteDelegate _canExecDel;
        public ListItemCommand(ExecuteDelegate executeDel, CanExecuteDelegate canExecDel = null)
        {
            this._executeDel = executeDel;
            this._canExecDel = canExecDel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecDel == null)
                return true;
            return _canExecDel((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeDel((T)parameter);
        }
    }
}
