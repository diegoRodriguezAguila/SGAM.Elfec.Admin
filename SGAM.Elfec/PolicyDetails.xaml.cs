using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
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
    }
}
