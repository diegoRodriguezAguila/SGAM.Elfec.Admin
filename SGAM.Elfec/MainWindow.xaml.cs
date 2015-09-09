using Fluent;
using SGAM.Elfec.CustomUI;
using SGAM.Elfec.Interfaces;
using SGAM.Elfec.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ChangeToAllAppsView();
            Loaded += buttonGreen_Click;
            //new LoginWindow().ShowDialog();
        }

        #region Constants
        public const string SELECTED_TAG = "Selected";
        public const string MAIN_TITLE = "SGAM Elfec";

        #endregion

        #region Private Methods
        private void ChangePrincipalView(string btnShowAppsTag, string btnShowMobilesTag, string btnShowGroupsTag, Control view)
        {
            BtnShowApps.Tag = btnShowAppsTag;
            BtnShowMobiles.Tag = btnShowMobilesTag;
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
            ChangeToAllMobileDevicesView();
        }

        private void BtnShowGroups_Click(object sender, RoutedEventArgs e)
        {
            ChangeToAllDeviceGroupsView();
        }

        #endregion

        #region Public Methods
        public void CloseWindow()
        {
            this.Close();
        }

        public void ChangeTitle(string title=null)
        {
            Title = (title != null ? (title + " - ") : "") + MAIN_TITLE;
        }

        public void ChangeToAllAppsView()
        {
            if ((string)BtnShowApps.Tag != SELECTED_TAG)
                ChangePrincipalView(SELECTED_TAG, null, null, new ShowAllApps());
        }

        public void ChangeToAllMobileDevicesView()
        {
            if ((string)BtnShowMobiles.Tag != SELECTED_TAG)
                ChangePrincipalView(null, SELECTED_TAG, null, new ShowAllMobileDevices());
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
            var loginDialog = new LoginDialogWindow();
            loginDialog.Owner = this;
            loginDialog.ShowDialog();
        }
    }
}
