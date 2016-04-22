using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Services;
using System.Windows.Controls;

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
    }
}
