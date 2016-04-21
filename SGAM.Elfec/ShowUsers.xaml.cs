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
    /// Interaction logic for ShowAllDeviceGroups.xaml
    /// </summary>
    public partial class ShowUsers : UserControl, IShowUsersView
    {
        private LoadingControl _indeterminateLoading;
        private ErrorControl _errorMessage;

        public ShowUsers()
        {
            InitializeComponent();
            _indeterminateLoading = new LoadingControl();
            _indeterminateLoading.Margin = new Thickness(40, 40, 0, 0);
            _errorMessage = new ErrorControl();
            _errorMessage.Margin = new Thickness(40, 40, 0, 0);
            DataContext = new ShowUsersPresenter(this);
        }

        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                if (Transitioning.Content != ListViewUsers)
                    Transitioning.Content = ListViewUsers;
            });
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.Message = Properties.Resources.MsgLoadingUsers;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgLoadingUsers);
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
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = ListViewUsers; };
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        #endregion
    }
}
