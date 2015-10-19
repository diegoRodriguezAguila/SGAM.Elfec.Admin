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
        public const string SELECTED_TAG = "Selected";
        public const string MAIN_TITLE = "SGAM Elfec";

        #endregion

        #region Private Methods
        private void ChangePrincipalView(string btnShowAppsTag, string btnShowMobilesTag, string btnShowGroupsTag, Control view)
        {
            BtnShowApps.Tag = btnShowAppsTag;
            BtnShowDevices.Tag = btnShowMobilesTag;
            BtnShowUsers.Tag = btnShowGroupsTag;
            MainWindowService.Instance.Navigation.Clear();
            ChangeToView(view);
        }

        private void BtnShowApps_Click(object sender, RoutedEventArgs e)
        {
            ChangeToAllAppsView();
        }

        private void BtnShowMobiles_Click(object sender, RoutedEventArgs e)
        {
            ChangeToDevicesView();
        }

        private void BtnShowGroups_Click(object sender, RoutedEventArgs e)
        {
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

        public void ChangeToAllAppsView(bool force = false)
        {
            if ((string)BtnShowApps.Tag != SELECTED_TAG || force)
                ChangePrincipalView(SELECTED_TAG, null, null, new ShowApplications());
        }

        public void ChangeToDevicesView(bool force = false)
        {
            if ((string)BtnShowDevices.Tag != SELECTED_TAG || force)
                ChangePrincipalView(null, SELECTED_TAG, null, new ShowDevices());
        }

        public void ChangeToAllDeviceGroupsView(bool force = false)
        {
            if ((string)BtnShowUsers.Tag != SELECTED_TAG || force)
                ChangePrincipalView(null, null, SELECTED_TAG, new ShowAllDeviceGroups());
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
            addNewApp.Tag = Properties.Resources.LblAddNewApp;
            MainWindowService.Instance.MainWindowView.ChangeToView(addNewApp);
            //var app = ApkManager.GetApplication("/Lecturas.Elfec.GD.apk");
        }
    }
}
