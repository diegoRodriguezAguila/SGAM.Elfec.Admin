using Fluent;
using SGAM.Elfec.Interfaces;
using SGAM.Elfec.Presenters.Presentation.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ObservableCollectionExtensions.Init();
            MainWindowService.Instance.MainWindow = this;
            //_isFirstActivated = true;
            DevicesView();
            DataContext = MainWindowService.Instance;
            /*Activated += (s, e) =>
            {
                if (_isFirstActivated)
                {
                    _isFirstActivated = false;
                    ShowLoginDialog();
                }
            };*/
        }

        #region Private Variables
        //private bool _isFirstActivated;
        #endregion

        #region Private Methods
        private void ChangePrincipalView(Control view)
        {
            MainWindowService.Instance.Navigation.Clear();
            CurrentView(view);
        }

        private void BtnShowApps_Click(object sender, RoutedEventArgs e)
        {
            ApplicationsView();
        }

        private void BtnShowDevices_Click(object sender, RoutedEventArgs e)
        {
            DevicesView();
        }

        private void BtnShowUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersView();
        }

        private void ShowLoginDialog(bool closeIfCanceled = true)
        {
            var loginDialog = new LoginDialogWindow();
            loginDialog.Owner = this;
            if (closeIfCanceled)
                loginDialog.LoginCanceled += (s, e) => { Close(); };
            loginDialog.UserLoggedIn += (s, u) => { DevicesView(); };
            loginDialog.ShowDialog();
        }
        #endregion

        #region Public Methods
        public void CloseWindow()
        {
            this.Close();
        }

        public IMainWindow ApplicationsView(bool force = false)
        {
            BtnShowApps.IsSelected = true;
            BtnViewApps.IsChecked = true;
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = false;
            bool shouldChange = !(MainWindowService.Instance.Navigation.Current is ShowApplications);
            if (shouldChange || force)
                ChangePrincipalView(new ShowApplications());
            return this;
        }

        public IMainWindow DevicesView(bool force = false)
        {

            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = true;
            BtnViewDevices.IsChecked = true;
            BtnShowUsers.IsSelected = false;
            bool shouldChange = !(MainWindowService.Instance.Navigation.Current is ShowDevices);
            if (shouldChange || force)
                ChangePrincipalView(new ShowDevices());
            return this;
        }

        public IMainWindow UsersView(bool force = false)
        {
            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = true;
            BtnViewUsers.IsChecked = true;
            bool shouldChange = !(MainWindowService.Instance.Navigation.Current is ShowUsers);
            if (shouldChange || force)
                ChangePrincipalView(new ShowUsers());
            return this;
        }

        public IMainWindow CurrentView<T>(T view) where T : Control
        {
            MainWindowService.Instance.Navigation.Add(view);
            return this;
        }

        public IMainWindow Back()
        {
            MainWindowService.Instance.Navigation.Back();
            return this;
        }

        public IMainWindow Forward()
        {
            MainWindowService.Instance.Navigation.Forward();
            return this;
        }

        public IMainWindow StatusBar(string status)
        {
            TxtStatus.Text = status;
            return this;
        }

        public IMainWindow StatusBarDefault()
        {
            TxtStatus.Text = Properties.Resources.LblStatusbarDefault;
            return this;
        }

        public IMainWindow NotifyUser(string title, string message)
        {
            Toast.Title = title;
            Toast.Message = message;
            return this;
        }

        #endregion

        private void BtnAddApp_Click(object sender, RoutedEventArgs e)
        {
            var addNewApp = new AddNewApplication();
            MainWindowService.Instance.MainWindow.CurrentView(addNewApp);
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var addNewUser = new AddNewUser();
            MainWindowService.Instance.MainWindow.CurrentView(addNewUser);
        }

        private void BtnAddUserGroup_Click(object sender, RoutedEventArgs e)
        {
            var addNewUserGroup = new AddNewUserGroup();
            MainWindowService.Instance.MainWindow.CurrentView(addNewUserGroup);
        }

        private void BtnAddPolicyRule_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainWindowService.Instance.Navigation.Current is PolicyRules))
            {
                var policyRule = new PolicyRules();
                MainWindowService.Instance.MainWindow.CurrentView(policyRule);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }

        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            Forward();
        }

        public IMainWindow SetCursor(Cursor mouse)
        {
            Mouse.OverrideCursor = mouse;
            return this;
        }

        public IMainWindow DefaultCursor()
        {

            Mouse.OverrideCursor = null;
            return this;
        }

        private void BtnSwitchUser_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowLoginDialog(false);
        }
    }
}
