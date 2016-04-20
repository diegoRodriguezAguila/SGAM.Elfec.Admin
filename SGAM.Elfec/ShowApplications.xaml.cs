using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowAllApps.xaml
    /// </summary>
    public partial class ShowApplications : UserControl, IShowApplicationsView
    {
        private IndeterminateLoading _indeterminateLoading;
        private ErrorControl _errorMessage;

        public ShowApplications()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _indeterminateLoading.Margin = new Thickness(40, 40, 0, 0);
            _errorMessage = new ErrorControl();
            _errorMessage.Margin = new Thickness(40, 40, 0, 0);
            DataContext = new ShowApplicationsPresenter(this);
        }



        #region Interface Methods

        public void ShowApplicationDetails(Model.Application application)
        {
            var showAppDetails = new ShowApplicationDetails(application);
            MainWindowService.Instance.MainWindow.CurrentView(showAppDetails);
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingApps;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgLoadingApps);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    MainWindowService.Instance.MainWindow.StatusBarDefault();
                    _errorMessage.Message = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                if (Transitioning.Content != ListViewApplications)
                    Transitioning.Content = ListViewApplications;
            });
        }

        #endregion
    }
}
