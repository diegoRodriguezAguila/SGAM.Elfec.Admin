using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddNewWhitelist.xaml
    /// </summary>
    public partial class PolicyRules : UserControl, IPolicyRulesView
    {
        public PolicyRules()
        {
            InitializeComponent();
            DataContext = new PolicyRulesPresenter(this);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
            MainWindowService.Instance.MainWindowView.GoBack();
        }
    }
}
