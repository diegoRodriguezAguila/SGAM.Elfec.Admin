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
        private LoadingControl _indeterminateLoading;
        private ProgressLoading _progressLoading;
        private ErrorControl _errorMessage;

        public AddNewApplication()
        {
            InitializeComponent();
            _indeterminateLoading = new LoadingControl { Orientation = Orientation.Vertical };
            _errorMessage = new ErrorControl();
            _progressLoading = new ProgressLoading();
            DataContext = new AddNewApplicationPresenter(this);
            var binding = new Binding("UploadProgressPercentage");
            binding.Source = DataContext;
            BindingOperations.SetBinding(_progressLoading.ProgressBarLoading, ProgressBar.ValueProperty, binding);
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
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.Message = Properties.Resources.MsgLoadingApk;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgLoadingApkShort);
                TransitioningLoadApk.Content = _indeterminateLoading;
            });
        }

        public void ShowAPKLoadError(Exception error)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _errorMessage.Message = error.Message;
                _errorMessage.BtnOk.Click += (s, o) => { OnAPKLoadFinished(); };
                TransitioningLoadApk.Content = _errorMessage;
            });
        }

        public void OnAPKLoadFinished()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                TransitioningLoadApk.Content = BtnBrowseApk;
            });
        }

        public void ShowUploadingAplication()
        {
            Dispatcher.InvokeAsync(() =>
            {
                _progressLoading.ProgressBarLoading.Value = 0;
                _progressLoading.Message = Properties.Resources.MsgUploadingApk;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgUploadingApk);
                TransitioningUpload.Content = _progressLoading;
            });
        }

        public void ShowAplicationUploadError(Exception error)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _errorMessage.Message = error.Message;
                _errorMessage.BtnOk.Click += (s, o) =>
                {
                    MainWindowService.Instance.MainWindow.StatusBarDefault();
                    TransitioningUpload.Content = null;
                };
                TransitioningUpload.Content = _errorMessage;
            });
        }

        public void ShowAplicationUploadedSuccessfully(Model.Application application)
        {
            Dispatcher.InvokeAsync(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindow;
                mainWindow.StatusBarDefault();
                mainWindow.ApplicationsView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    String.Format(Properties.Resources.MsgApplicationUploadedSuccessfully, application.Name));
            });
        }

        #endregion
    }
}
