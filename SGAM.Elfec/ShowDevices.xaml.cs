using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowDevices.xaml
    /// </summary>
    public partial class ShowDevices : UserControl, IShowDevicesView
    {
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public ShowDevices()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _indeterminateLoading.Margin = new Thickness(40, 40, 0, 0);
            _errorMessage = new ErrorMessage();
            _errorMessage.Margin = new Thickness(40, 40, 0, 0);
            DataContext = new ShowDevicesPresenter(this);
        }

        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                if (Transitioning.Content != ListViewDevices)
                    Transitioning.Content = ListViewDevices;
            });
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    MainWindowService.Instance.MainWindow.StatusBarDefault();
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingDevices;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgLoadingDevices);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void ViewDeviceAuthorization(Device device)
        {
            var authDeviceView = new AuthorizeDevice(ObjectCloner.Clone(device));
            MainWindowService.Instance.MainWindow.CurrentView(authDeviceView);
        }

        #endregion


    }
}
