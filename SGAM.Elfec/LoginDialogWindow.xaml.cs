using Fluent;
using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginDialogWindow : RibbonWindow, ILoginView
    {
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
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Transitioning.Content = LoginPanel;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ShowWaiting();
             //Presenter.Login();
             Thread timer = new Thread(() => 
            {
                Thread.Sleep(6000);
                Transitioning.Dispatcher.Invoke(new Action(()=> 
                {
                    _errorMessage.TxtErrorMessage.Text = "Error el pinche usuario que quiere acceder esta drogado o algo asi bien chido!";
                    Transitioning.Content = _errorMessage; }));
                });
            timer.Start();
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
            _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(validationErrors);
            Transitioning.Content = _errorMessage;
        }

        public void ShowWaiting()
        {
            _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoginUser;
            Transitioning.Content = _indeterminateLoading;
        }

        public void UpdateWaiting(string waitingMessage)
        {
            _indeterminateLoading.TxtLoadingMessage.Text = waitingMessage;
        }

        public void HideWaiting()
        {
            Transitioning.Content = null;
        }

        public void ShowErrorsInUsernameField(IList<Exception> errors)
        {
            TxtUsernameError.Visibility = Visibility.Visible;
            TxtUsernameError.Content = MessageListFormatter.FormatFromErrorList(errors);
        }

        public void ShowErrorsInPasswordField(IList<Exception> errors)
        {
            TxtPasswordError.Visibility = Visibility.Visible;
            TxtPasswordError.Content = MessageListFormatter.FormatFromErrorList(errors);
        }

        #endregion
    }
}
