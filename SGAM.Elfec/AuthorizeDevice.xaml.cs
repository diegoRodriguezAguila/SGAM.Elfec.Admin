using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AuthorizeDevice.xaml
    /// </summary>
    public partial class AuthorizeDevice : UserControl, IAuthorizeDeviceView
    {
        private AuthorizeDevicePresenter _presenter;
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public AuthorizeDevice(Device authPendingDevice)
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _errorMessage = new ErrorMessage();
            _presenter = new AuthorizeDevicePresenter(this, authPendingDevice);
            DataContext = _presenter.AuthPendingDevice;
        }

        private void BtnAuthorize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _presenter.AuthorizeDevice();
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.GoBack();
        }


        #region Interface Methods

        public void ShowProcesingAuthorization()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgAuthorizingDevice;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgAuthorizingDevice);
                Transitioning.Content = _indeterminateLoading;
            }));
        }

        public void ShowAuthorizationErrors(params Exception[] errors)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                Transitioning.Content = _errorMessage;
            }));
        }

        public void ShowDeviceAuthorizedSuccessfuly(Device device)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindowView;
                mainWindow.SetStatusBarDefault();
                mainWindow.ChangeToDevicesView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess, 
                    String.Format(Properties.Resources.MsgDeviceSuccessfullyAuthorized, device.Name));
            }));
        }

        #endregion
    }
}
