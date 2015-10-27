using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            var binding = new Binding("UploadProgressPercentage");
            binding.Source = DataContext;
            BindingOperations.SetBinding(_progressLoading.ProgressBarLoading, ProgressBar.ValueProperty, binding);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
            MainWindowService.Instance.MainWindowView.GoBack();
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

        public void ShowUploadingAplication()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _progressLoading.TxtLoadingMessage.Text = Properties.Resources.MsgUploadingApk;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgUploadingApk);
                TransitioningUpload.Content = _progressLoading;
            }));
        }

        public void ShowAplicationUploadErrors(params Exception[] errors)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                _errorMessage.BtnOk.Click += (s, o) =>
                {
                    MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                    TransitioningUpload.Content = null;
                };
                TransitioningUpload.Content = _errorMessage;
            }));
        }

        public void ShowAplicationUploadedSuccessfully(Model.Application application)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindowView;
                mainWindow.SetStatusBarDefault();
                mainWindow.ChangeToApplicationsView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    String.Format(Properties.Resources.MsgApplicationUploadedSuccessfully, application.Name));
            }));
        }

        #endregion
    }
}
