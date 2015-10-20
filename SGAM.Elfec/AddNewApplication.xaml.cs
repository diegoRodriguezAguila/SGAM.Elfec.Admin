using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddNewApplication.xaml
    /// </summary>
    public partial class AddNewApplication : UserControl, IAddNewApplicationView
    {
        public AddNewApplication()
        {
            InitializeComponent();
            DataContext = new AddNewApplicationPresenter(this);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.GoBack();
        }

        private void BtnAddApp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBrowseApk_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = Properties.Resources.FileAPKFilter };
            var result = ofd.ShowDialog();
            if (result == false) return;
            TxtApkFilename.Text = ofd.FileName;
        }
    }
}
