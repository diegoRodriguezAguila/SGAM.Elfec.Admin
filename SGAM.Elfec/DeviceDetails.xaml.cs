using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for DeviceDetails.xaml
    /// </summary>
    public partial class DeviceDetails : UserControl, IDeviceDetailsView
    {
        public DeviceDetails(Device device)
        {
            InitializeComponent();
            DataContext = new DeviceDetailsPresenter(this, device);
        }
    }
}
