using SGAM.Elfec.Interfaces;
using System;
using System.Windows;

namespace SGAM.Elfec.Services.Dialogs
{
    /// <summary>
    /// Builder para un dialogo de confirmación
    /// </summary>
    public class ConfirmationDialogBuilder : IConfirmationDialogBuilder
    {
        private string _title;
        private string _message;
        private UIElement _content;
        private Action _cancelAction;
        private Action _confirmAction;

        public IDialog Build()
        {
            var dialog = new ConfirmationDialog
            {
                Message = _message,
                ConfirmationContent = _content
            };
            if (_title != null)
                dialog.Title = _title;
            if (_cancelAction != null)
                dialog.Canceled += (s, e) => { _cancelAction(); };
            if (_confirmAction != null)
                dialog.Canceled += (s, e) => { _confirmAction(); };
            return dialog;
        }

        public IConfirmationDialogBuilder SetContent(UIElement content)
        {
            _content = content;
            return this;
        }

        public IConfirmationDialogBuilder SetMessage(string message)
        {
            _message = message;
            return this;
        }

        public IConfirmationDialogBuilder SetOnCancel(Action action)
        {
            _cancelAction = action;
            return this;
        }

        public IConfirmationDialogBuilder SetOnConfirm(Action action)
        {
            _confirmAction = action;
            return this;
        }

        public IConfirmationDialogBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }
    }
}
