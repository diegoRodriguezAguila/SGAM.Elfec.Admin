using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddNewApplication.xaml
    /// </summary>
    public partial class AddNewApplication : UserControl, IAddNewApplicationView
    {
        private IndeterminateLoading _indeterminateLoading;
        private ProgressLoading _progressLoading;
        private ErrorMessage _errorMessage;

        public AddNewApplication()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _errorMessage = new ErrorMessage();
            _progressLoading = new ProgressLoading();
            DataContext = new AddNewApplicationPresenter(this);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
            MainWindowService.Instance.MainWindowView.GoBack();
        }

        private void BtnAddApp_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("X-Api-Token", "iYxx6xuQY_DDxornWgbA");
            webClient.Headers.Add("X-Api-Username", "drodriguezd");
            webClient.UploadFileAsync(new Uri("http://192.168.50.56:3000/api/applications"), TxtApkFilename.Text);
            webClient.UploadProgressChanged += (s, ev) =>
            {
                _progressLoading.ProgressBarLoading.Value = ev.ProgressPercentage;
            };
            _progressLoading.TxtLoadingMessage.Text = Properties.Resources.MsgUploadingApk;
            TransitioningUpload.Content = _progressLoading;
        }

        private void BtnBrowseApk_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = Properties.Resources.FileAPKFilter };
            var result = ofd.ShowDialog();
            if (result == false) return;
            TxtApkFilename.Text = ofd.FileName;
        }

        #region Interface Methods

        public void ShowLoadingAPK()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingApk;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgLoadingApkShort);
                TransitioningLoadApk.Content = _indeterminateLoading;
            }));
        }

        public void ShowAPKLoadErrors(params Exception[] errors)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                _errorMessage.BtnOk.Click += (s, o) => { OnAPKLoadFinished(); };
                TransitioningLoadApk.Content = _errorMessage;
            }));
        }

        public void OnAPKLoadFinished()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                TransitioningLoadApk.Content = BtnBrowseApk;
            }));
        }

        #endregion
    }
}
