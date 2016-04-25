using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Services;
using SGAM.Elfec.Services.Dialogs;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for PolicyDetails.xaml
    /// </summary>
    public partial class PolicyDetails : UserControl, IPolicyDetailsView
    {
        public PolicyDetails(Policy policy)
        {
            InitializeComponent();
            DataContext = new PolicyDetailsPresenter(this, policy);
        }

        public void AddRule(Policy policy)
        {
            var addRuleView = new AddRule(policy);
            var dialog = DialogBuilder.For(addRuleView).Build();
            addRuleView.AddRuleSuccess += (s, e) => { dialog.SetDialogResult(true); };
            dialog.ShowDialog();
        }

        public bool DeleteConfirmation()
        {
            return (bool)new ConfirmationDialogBuilder()
                .SetTitle(Properties.Resources.TitleDeleteRule)
                .SetMessage(Properties.Resources.MsgDeleteRuleConfirmation)
                .Build()
                .ShowDialog();
        }

        public void DeletingRule()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.
                StatusBar(Properties.Resources.MsgDeletingRule)
                .SetCursor(Cursors.AppStarting);
            });
        }

        public void ErrorDeleting(Exception error)
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
            });
        }

        public void RuleDeleted()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow
                .StatusBarDefault()
                .DefaultCursor();
            });
        }
    }
}
