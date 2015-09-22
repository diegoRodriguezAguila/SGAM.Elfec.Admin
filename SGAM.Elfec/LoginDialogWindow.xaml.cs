using Fluent;
using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SGAM.Elfec
{

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginDialogWindow : RibbonWindow, ILoginView
    {
        public object Text { get; set; }
        private LoginPresenter Presenter { get; set; }
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;


        public LoginDialogWindow()
        {
            InitializeComponent();
            Presenter = new LoginPresenter(this);
            _indeterminateLoading = new IndeterminateLoading();
            _indeterminateLoading.Margin = new Thickness(0, 20, 0, 0);
            _errorMessage = new ErrorMessage();
            _errorMessage.BtnOk.Click += BtnOk_Click;
            DataContext = this;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Transitioning.Content = LoginPanel;
            TxtPassword.Clear();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool validUsername = IsUsernameValid();
            bool validPassword = IsPasswordValid();
            if (validUsername && validPassword)
                Presenter.LogIn();
        }

        private bool IsFieldValid(Control txtField, Label lblError)
        {
            IList<ValidationError> errors = Validation.GetErrors(txtField);
            bool hasErrors = errors.Count > 0;
            lblError.Visibility = hasErrors ? Visibility.Visible : Visibility.Collapsed;
            if (hasErrors)
                lblError.Content = MessageListFormatter
                    .FormatFromObjectList(errors, (e) => { return e.ErrorContent.ToString(); });
            return !hasErrors;
        }


        private bool IsUsernameValid()
        {
            BindingExpression be = TxtUsername.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
            be.UpdateSource();
            bool res = IsFieldValid(TxtUsername, LblUsernameError);
            Validation.ClearInvalid(be);
            return res;
        }

        private bool IsPasswordValid()
        {
            TxtDummyPassword.Text = TxtPassword.Password;
            object a = Text;
            BindingExpression be = TxtDummyPassword.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
            be.UpdateSource();
            bool res = IsFieldValid(TxtDummyPassword, LblPasswordError);
            TxtDummyPassword.Text = null;
            return res;
        }

        #region Interface Methods

        public string Username
        {
            get { return TxtUsername.Text.Trim().ToLower(); }
        }

        public string Password
        {
            get { return TxtPassword.Password; }
        }

        public void ShowLoginErrors(IList<Exception> validationErrors)
        {
            if (validationErrors.Count > 0)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(validationErrors);
                    Transitioning.Content = _errorMessage;
                }));
            }
        }

        public void ShowWaiting()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoginUser;
                Transitioning.Content = _indeterminateLoading;
            }));
        }

        public void UpdateWaiting(string waitingMessage)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = waitingMessage;
            }));
        }

        public void HideWaiting()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Transitioning.Content = null;
            }));
        }

        #endregion
    }
}
