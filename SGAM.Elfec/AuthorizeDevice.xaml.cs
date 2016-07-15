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
        private LoadingControl _indeterminateLoading;
        private ErrorControl _errorMessage;

        public AuthorizeDevice(Device authPendingDevice)
        {
            InitializeComponent();
            _indeterminateLoading = new LoadingControl();
            _errorMessage = new ErrorControl();
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
                _indeterminateLoading.Message = Properties.Resources.MsgAuthorizingDevice;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgAuthorizingDevice);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void ShowAuthorizationError(Exception error)
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                _errorMessage.Message = error.Message;
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
            _errorMessage.Message = Properties.Resources.MsgErrorsInFields;
            Transitioning.Content = _errorMessage;
        }

        #endregion
    }
}
