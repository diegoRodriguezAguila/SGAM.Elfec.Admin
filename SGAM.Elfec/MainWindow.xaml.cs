using Fluent;
using SGAM.Elfec.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindowService
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowService.Instance.MainWindowView = this;
            _isFirstActivated = true;
            DevicesView();
            // Activated += (s, e) => { ShowLoginDialog(); };
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

        private void ShowLoginDialog()
        {
            if (_isFirstActivated)
            {
                _isFirstActivated = false;
                var loginDialog = new LoginDialogWindow();
                loginDialog.Owner = this;
                loginDialog.LoginCanceled += (s, e) => { Close(); };
                loginDialog.UserLoggedIn += (s, u) => { DevicesView(); };
                loginDialog.ShowDialog();
            }
        }

        /// <summary>
        /// Realiza un binding del tag de un control de UI al titulo de la ventana principal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        private void BindTitle<T>(T view) where T : Control
        {
            BindingOperations.ClearBinding(this, Window.TitleProperty);
            Binding binding = new Binding();
            binding.StringFormat = "{0} - " + MAIN_TITLE;
            binding.TargetNullValue = MAIN_TITLE;
            binding.Path = new PropertyPath(Control.TagProperty);
            binding.Source = view;
            BindingOperations.SetBinding(this, Window.TitleProperty, binding);
        }

        #endregion

        #region Public Methods
        public void CloseWindow()
        {
            this.Close();
        }

        public IMainWindowService WindowTitle(string title = null)
        {
            base.Title = (title != null ? (title + " - ") : "") + MAIN_TITLE;
            return this;
        }

        public IMainWindowService ApplicationsView(bool force = false)
        {
            BtnShowApps.IsSelected = true;
            BtnViewApps.IsChecked = true;
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = false;
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowApplications);
            if (shouldChange || force)
                ChangePrincipalView(new ShowApplications());
            return this;
        }

        public IMainWindowService DevicesView(bool force = false)
        {

            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = true;
            BtnViewDevices.IsChecked = true;
            BtnShowUsers.IsSelected = false;
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowDevices);
            if (shouldChange || force)
                ChangePrincipalView(new ShowDevices());
            return this;
        }

        public IMainWindowService UsersView(bool force = false)
        {
            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = true;
            BtnViewUsers.IsChecked = true;
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowUsers);
            if (shouldChange || force)
                ChangePrincipalView(new ShowUsers());
            return this;
        }

        public IMainWindowService CurrentView<T>(T view) where T : Control
        {
            if (MainWindowService.Instance.Navigation.Count == 0
                || !(MainWindowService.Instance.Navigation.Peek() is T))
            {
                MainWindowService.Instance.Navigation.Push(view);
                InnerContent.Content = view;
                BindTitle(view);
            }
            return this;
        }

        public IMainWindowService Back()
        {
            if (MainWindowService.Instance.Navigation.Count > 1)
            {
                MainWindowService.Instance.Navigation.Pop();
                var view = MainWindowService.Instance.Navigation.Peek();
                InnerContent.Content = view;
                BindTitle(view);
            }
            return this;
        }

        public IMainWindowService StatusBar(string status)
        {
            TxtStatus.Text = status;
            return this;
        }

        public IMainWindowService StatusBarDefault()
        {
            TxtStatus.Text = Properties.Resources.LblStatusbarDefault;
            return this;
        }

        public IMainWindowService NotifyUser(string title, string message)
        {
            Toast.Title = title;
            Toast.Message = message;
            return this;
        }

        #endregion

        private void BtnAddApp_Click(object sender, RoutedEventArgs e)
        {
            var addNewApp = new AddNewApplication();
            MainWindowService.Instance.MainWindowView.CurrentView(addNewApp);
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var addNewUser = new AddNewUser();
            MainWindowService.Instance.MainWindowView.CurrentView(addNewUser);
        }

        private void BtnAddUserGroup_Click(object sender, RoutedEventArgs e)
        {
            var addNewUserGroup = new AddNewUserGroup();
            MainWindowService.Instance.MainWindowView.CurrentView(addNewUserGroup);
        }

        private void BtnAddPolicyRule_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainWindowService.Instance.CurrentView is PolicyRules))
            {
                var policyRule = new PolicyRules();
                MainWindowService.Instance.MainWindowView.CurrentView(policyRule);
            }
        }
    }
}
