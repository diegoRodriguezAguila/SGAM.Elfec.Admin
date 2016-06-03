using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;

namespace SGAM.Elfec.Presenters
{
    public class DeviceDetailsPresenter : BasePresenter<IDeviceDetailsView>
    {
        public DeviceDetailsPresenter(IDeviceDetailsView view,
            Device device) : base(view)
        {
            Device = device;
        }

        #region Private Attributes
        private Device _device;
        #endregion

        #region Properties
        public Device Device
        {
            get { return _device; }
            set
            {
                _device = value;
                RaisePropertyChanged("Device");
            }
        }

        #endregion
    }
}
