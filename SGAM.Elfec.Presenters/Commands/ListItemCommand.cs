using System;
using System.Collections;
using System.Collections.Generic;
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
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67
        public bool CanExecute(object parameter)
        {
            if (_canExecDel == null)
                return true;
            return _canExecDel((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (typeof(T) == typeof(IList) &&
                !(parameter is IList))
            {
                IList list = new List<object>();
                list.Add(parameter);
                _executeDel((T)list);
            }
            else _executeDel((T)parameter);
        }
    }
}
