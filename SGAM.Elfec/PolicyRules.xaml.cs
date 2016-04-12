using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddNewWhitelist.xaml
    /// </summary>
    public partial class PolicyRules : UserControl, IPolicyRulesView
    {
        public PolicyDetails PolicyDetailsView { get; set; }

        public PolicyRules()
        {
            InitializeComponent();
            DataContext = new PolicyRulesPresenter(this);
            PolicyDetailsView = new PolicyDetails(null);
        }

        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                Mouse.OverrideCursor = null;
            });
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow
                .StatusBar(Properties.Resources.MsgLoadingUsers);
                Mouse.OverrideCursor = Cursors.AppStarting;
            });
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    MainWindowService.Instance.MainWindow.StatusBarDefault();
                    Mouse.OverrideCursor = null;
                });
            }
        }

        public void PolicyDetails(Policy policy)
        {
            Dispatcher.InvokeAsync(() =>
            {
                ((PolicyDetailsPresenter)PolicyDetailsView.DataContext).Policy = policy;
                TransitioningDetails.Content = PolicyDetailsView;
            });
        }

        #endregion
    }
}
