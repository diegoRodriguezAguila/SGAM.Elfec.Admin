using Fluent;
using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using SGAM.Elfec.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    public delegate void UserLoggedInEventHandler(object sender, User loggedUser);
    public delegate void LoginCanceledEventHandler(object sender, EventArgs e);
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginDialogWindow : RibbonWindow, ILoginView
    {
        #region Events

        public event UserLoggedInEventHandler UserLoggedIn;
        public event LoginCanceledEventHandler LoginCanceled;
        #endregion

        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;
        private bool _manualClosing;


        public LoginDialogWindow()
        {
            InitializeComponent();
            DataContext = new LoginPresenter(this);
            _indeterminateLoading = new IndeterminateLoading();
            _indeterminateLoading.Margin = new Thickness(0, 20, 0, 0);
            _errorMessage = new ErrorMessage();
            var varRes = new ValidationResult(true, null);
            _errorMessage.BtnOk.Click += BtnOk_Click;
            Loaded += (s, e) => { ClearErrors(); };
            Closed += (s, e) =>
            {
                if (!_manualClosing && LoginCanceled != null)
                    LoginCanceled(s, e);
            };
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Transitioning.Content = LoginPanel;
            TxtPassword.Clear();
            ClearErrors();
        }

        #region Interface Methods

        public void ShowLoginErrors(params Exception[] validationErrors)
        {
            if (validationErrors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(validationErrors);
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        public void ShowWaiting()
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoginUser;
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void UpdateWaiting(string waitingMessage)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = waitingMessage;
            });
        }

        public void HideWaiting()
        {
            Dispatcher.InvokeAsync(() =>
            {
                Transitioning.Content = null;
            });
        }

        public void NotifySuccessfulLogin(User loggedUser)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _manualClosing = true;
                Close();
                _manualClosing = false;
                if (UserLoggedIn != null)
                    UserLoggedIn(this, loggedUser);
            });
        }

        #endregion

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ValidationErrorsAssistant.UpdateSources(LoginPanel);
        }

        private void ClearErrors()
        {
            ValidationErrorsAssistant.ClearErrors(LoginPanel);
        }
    }
}
