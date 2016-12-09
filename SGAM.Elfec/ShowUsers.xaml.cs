using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Interfaces;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Services.Dialogs;
using SGAM.Elfec.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowAllDeviceGroups.xaml
    /// </summary>
    public partial class ShowUsers : UserControl, IShowUsersView, ISearchable
    {
        private LoadingControl _indeterminateLoading;
        private ErrorControl _errorMessage;

        public ShowUsers()
        {
            InitializeComponent();
            _indeterminateLoading = new LoadingControl { Margin = new Thickness(40, 40, 0, 0) };
            _errorMessage = new ErrorControl { Margin = new Thickness(40, 40, 0, 0) };
            DataContext = new ShowUsersPresenter(this);
        }

        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                if (Transitioning.Content != ListContent)
                    Transitioning.Content = ListContent;
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
            if (errors.Length == 0) return;
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                _errorMessage.Message = MessageListFormatter.FormatFromErrorList(errors);
                _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = ListContent; };
                Transitioning.Content = _errorMessage;
            });
        }

        public void ProcessingStatusChange()
        {
            MainWindowService.Instance.MainWindow.
                StatusBar(Properties.Resources.MsgUpdatingUserGroupStatus)
                .SetCursor(Cursors.AppStarting);
        }

        public void ErrorChangingStatus(Exception error)
        {
            MainWindowService.Instance.MainWindow
                .StatusBarDefault()
                .DefaultCursor();
            new InformationDialog
            {
                Title = Properties.Resources.TitleErrorInUserGroupStatusUpdate,
                Message = string.Format(Properties.Resources.MsgErrorInUserGroupStatusUpdate,
                    error.Message),
                IconType = IconType.Warning
            }.ShowDialog();
        }

        public void StatusChanged()
        {
            MainWindowService.Instance.MainWindow
                .StatusBarDefault()
                .DefaultCursor();
        }

        public void ShowUserDetails(User user)
        {
            var userDetails = new UserDetails(user);
            MainWindowService.Instance.MainWindow.CurrentView(userDetails);
        }

        #endregion

        #region ISearchable

        public void OnRequestSearch(object sender, RoutedEventArgs routedEventArgs)
        {
            if (IsVisible && !SearchPanel.IsOpened)
                SearchPanel.IsOpened = true;
        }

        public void OnCancelSearch(object sender, RoutedEventArgs routedEventArgs)
        {
            if (IsVisible && SearchPanel.IsOpened)
                SearchPanel.IsOpened = false;
        }

        #endregion

        public void ForceShowSearch()
        {
            SearchPanel.IsOpened = true;
        }

        private void ListBoxGroups_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchPanel.IsOpened = false;
        }
    }
}