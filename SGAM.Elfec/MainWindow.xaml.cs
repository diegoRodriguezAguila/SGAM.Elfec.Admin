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
            ChangeToDevicesView();
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
            ChangeToView(view);
        }

        private void BtnShowApps_Click(object sender, RoutedEventArgs e)
        {
            ChangeToApplicationsView();
        }

        private void BtnShowDevices_Click(object sender, RoutedEventArgs e)
        {
            ChangeToDevicesView();
        }

        private void BtnShowUsers_Click(object sender, RoutedEventArgs e)
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

        public void ChangeTitle(string title = null)
        {
            Title = (title != null ? (title + " - ") : "") + MAIN_TITLE;
        }

        public void ChangeToApplicationsView(bool force = false)
        {
            BtnShowApps.IsSelected = true;
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = false;
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowApplications);
            if (shouldChange || force)
                ChangePrincipalView(new ShowApplications());
        }

        public void ChangeToDevicesView(bool force = false)
        {

            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = true;
            BtnShowUsers.IsSelected = false;
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowDevices);
            if (shouldChange || force)
                ChangePrincipalView(new ShowDevices());
        }

        public void ChangeToAllDeviceGroupsView(bool force = false)
        {
            BtnShowApps.IsSelected = false;
            BtnShowDevices.IsSelected = false;
            BtnShowUsers.IsSelected = true;
            bool shouldChange = !(MainWindowService.Instance.CurrentView is ShowUsers);
            if (shouldChange || force)
                ChangePrincipalView(new ShowUsers());
        }

        public void ChangeToView<T>(T view) where T : Control
        {
            MainWindowService.Instance.Navigation.Push(view);
            InnerContent.Content = view;
            BindTitle(view);
        }

        public void GoBack()
        {
            if (MainWindowService.Instance.Navigation.Count > 1)
            {
                MainWindowService.Instance.Navigation.Pop();
                var view = MainWindowService.Instance.Navigation.Peek();
                InnerContent.Content = view;
                BindTitle(view);
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
