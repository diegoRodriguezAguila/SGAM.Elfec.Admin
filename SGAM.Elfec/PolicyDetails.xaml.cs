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

        private void BtnAddRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogBuilder.For(new AddNewUser()).Build().ShowDialog();
        }
    }
}
