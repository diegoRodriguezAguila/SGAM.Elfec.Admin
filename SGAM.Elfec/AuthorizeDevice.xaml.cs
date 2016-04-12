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
    /// <summary>
    /// Interaction logic for AuthorizeDevice.xaml
    /// </summary>
    public partial class AuthorizeDevice : UserControl, IAuthorizeDeviceView
    {
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public AuthorizeDevice(Device authPendingDevice)
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _errorMessage = new ErrorMessage();
            _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
            DataContext = new AuthorizeDevicePresenter(this, authPendingDevice);
            Loaded += (s, e) => { ValidationErrorsAssistant.ClearErrors(AuthPanel); };
        }

        private void BtnAuthorize_Click(object sender, RoutedEventArgs e)
        {
            ValidationErrorsAssistant.UpdateSources(AuthPanel);
        }

        #region Interface Methods

        public void ShowProcesingAuthorization()
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgAuthorizingDevice;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgAuthorizingDevice);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void ShowAuthorizationErrors(params Exception[] errors)
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                Transitioning.Content = _errorMessage;
            });
        }

        public void ShowDeviceAuthorizedSuccessfuly(Device device)
        {
            Dispatcher.InvokeAsync(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindow;
                mainWindow.StatusBarDefault();
                mainWindow.DevicesView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    String.Format(Properties.Resources.MsgDeviceSuccessfullyAuthorized, device.Name));
            });
        }

        public void NotifyErrorsInFields()
        {
            _errorMessage.TxtErrorMessage.Text = Properties.Resources.MsgErrorsInFields;
            Transitioning.Content = _errorMessage;
        }

        #endregion
    }
}
