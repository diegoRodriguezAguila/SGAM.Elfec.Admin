using System;
using System.Windows;

namespace SGAM.Elfec.Interfaces
{
    public interface IConfirmationDialogBuilder : IDialogBuilder
    {
        IConfirmationDialogBuilder SetOnConfirm(Action action);
        IConfirmationDialogBuilder SetMessage(string message);
        IConfirmationDialogBuilder SetTitle(string title);
        IConfirmationDialogBuilder SetContent(UIElement content);
        IConfirmationDialogBuilder SetOnCancel(Action action);
    }
}
