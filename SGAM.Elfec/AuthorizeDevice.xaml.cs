using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AuthorizeDevice.xaml
    /// </summary>
    public partial class AuthorizeDevice : UserControl, IAuthorizeDeviceView
    {
        private AuthorizeDevicePresenter _presenter;

        public AuthorizeDevice(Device authPendingDevice)
        {
            InitializeComponent();
            _presenter = new AuthorizeDevicePresenter(this, authPendingDevice);
            DataContext = _presenter.AuthPendingDevice;
        }

        private void BtnAuthorize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _presenter.AuthorizeDevice();
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.GoBack();
        }
    }
}
