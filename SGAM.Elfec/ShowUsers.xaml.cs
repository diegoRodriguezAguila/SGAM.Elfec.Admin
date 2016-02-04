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
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public ShowUsers()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _indeterminateLoading.Margin = new Thickness(40, 40, 0, 0);
            _errorMessage = new ErrorMessage();
            _errorMessage.Margin = new Thickness(40, 40, 0, 0);
            DataContext = new ShowUsersPresenter(this);
        }

        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                if (Transitioning.Content != ListViewUsers)
                    Transitioning.Content = ListViewUsers;
            });
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingUsers;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgLoadingUsers);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        #endregion
    }
}
