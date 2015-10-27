using Fluent;
using SGAM.Elfec.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindowView
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowService.Instance.MainWindowView = this;
            ChangeToDevicesView();
            _isFirstActivated = true;
            //Activated += (s, e) => { ShowLoginDialog(); };
        }

        #region Private Variables
        private bool _isFirstActivated;
        #endregion

        #region Constants
        public const string MAIN_TITLE = "SGAM Elfec";

        #endregion

        #region Private Methods
        private void ChangePrincipalView(Control view)
        {
            MainWindowService.Instance.Navigation.Clear();
            ChangeToView(view);
        }

        private void BtnShowApps_Click(object sender, RoutedEventArgs e)
        {
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = false;
            ChangeToApplicationsView();
        }

        private void BtnShowDevices_Click(object sender, RoutedEventArgs e)
        {
            BtnShowApps.IsSelected = false;
            BtnShowUsers.IsSelected = false;
            ChangeToDevicesView();
        }

        private void BtnShowUsers_Click(object sender, RoutedEventArgs e)
        {
            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = false;
            ChangeToAllDeviceGroupsView();
        }

        private void ShowLoginDialog()
        {
            if (_isFirstActivated)
            {
                _isFirstActivated = false;
                var loginDialog = new LoginDialogWindow();
                loginDialog.Owner = this;
                loginDialog.LoginCanceled += (s, e) => { Close(); };
                loginDialog.UserLoggedIn += (s, u) => { ChangeToDevicesView(); };
                loginDialog.ShowDialog();
            }
        }

        #endregion

        #region Public Methods
        public void CloseWindow()
        {
            this.Close();
        }

        public void ChangeTitle(string title = null)
        {
            Title = (title != null ? (title + " - ") : "") + MAIN_TITLE;
        }

        public void ChangeToApplicationsView(bool force = false)
        {
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowApplications);
            if (shouldChange || force)
                ChangePrincipalView(new ShowApplications());
        }

        public void ChangeToDevicesView(bool force = false)
        {
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowDevices);
            if (shouldChange || force)
                ChangePrincipalView(new ShowDevices());
        }

        public void ChangeToAllDeviceGroupsView(bool force = false)
        {
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowUsers);
            if (shouldChange || force)
                ChangePrincipalView(new ShowUsers());
        }

        public void ChangeToView<T>(T view) where T : Control
        {
            MainWindowService.Instance.Navigation.Push(view);
            InnerContent.Content = view;
            ChangeTitle(view.Tag as string);
        }

        public void GoBack()
        {
            if (MainWindowService.Instance.Navigation.Count > 1)
            {
                MainWindowService.Instance.Navigation.Pop();
                var view = MainWindowService.Instance.Navigation.Peek();
                InnerContent.Content = view;
                ChangeTitle(view.Tag as string);
            }
        }

        public void SetStatusBar(string status)
        {
            TxtStatus.Text = status;
        }

        public void SetStatusBarDefault()
        {
            TxtStatus.Text = Properties.Resources.LblStatusbarDefault;
        }

        public void NotifyUser(string title, string message)
        {
            Toast.Title = title;
            Toast.Message = message;
        }

        #endregion

        private void BtnAddApp_Click(object sender, RoutedEventArgs e)
        {
            var addNewApp = new AddNewApplication();
            MainWindowService.Instance.MainWindowView.ChangeToView(addNewApp);
        }
    }
}
