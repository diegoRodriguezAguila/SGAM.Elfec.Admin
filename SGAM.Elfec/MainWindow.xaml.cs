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
            BtnShowGroups.Tag = btnShowGroupsTag;
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

        public void ChangeToAllAppsView()
        {
            if ((string)BtnShowApps.Tag != SELECTED_TAG)
                ChangePrincipalView(SELECTED_TAG, null, null, new ShowAllApps());
        }

        public void ChangeToDevicesView()
        {
            if ((string)BtnShowDevices.Tag != SELECTED_TAG)
                ChangePrincipalView(null, SELECTED_TAG, null, new ShowDevices());
        }

        public void ChangeToAllDeviceGroupsView()
        {
            if ((string)BtnShowGroups.Tag != SELECTED_TAG)
                ChangePrincipalView(null, null, SELECTED_TAG, new ShowAllDeviceGroups());
        }

        public void ChangeToView<T>(T view) where T : Control
        {
            InnerContent.Content = view;
            MainWindowService.Instance.Navigation.Push(view);
            ChangeTitle(view.Tag as string);
        }

        public void GoBack()
        {
            if (MainWindowService.Instance.Navigation.Count > 0)
            {
                var view = MainWindowService.Instance.Navigation.Pop();
                InnerContent.Content = view;
                ChangeTitle(view.Tag as string);
            }
        }
        #endregion

        private void buttonGreen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
