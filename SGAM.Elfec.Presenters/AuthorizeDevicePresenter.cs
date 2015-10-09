using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class AuthorizeDevicePresenter : BasePresenter<IAuthorizeDeviceView>
    {
        public AuthorizeDevicePresenter(IAuthorizeDeviceView view, Device authPendingDevice) : base(view)
        {
            AuthPendingDevice = authPendingDevice;
        }

        #region Private Attributes
        private Device _authPendingDevice;
        #endregion

        #region Properties
        public Device AuthPendingDevice
        {
            get { return _authPendingDevice; }
            set
            {
                _authPendingDevice = value;
                RaisePropertyChanged("AuthPendingDevice");
            }
        }

        public ICommand AuthorizeDeviceCommand { get { return new DelegateCommand(AuthorizeDevice); } }

        #endregion

        private void AuthorizeDevice()
        {
            if (AuthPendingDevice.IsValid)
                new Thread(() =>
                {
                    View.ShowProcesingAuthorization();
                    var callback = new ResultCallback<Device>();
                    callback.Success += (s, device) =>
                    {
                        View.ShowDeviceAuthorizedSuccessfuly(device);
                    };
                    callback.Failure += (s, errors) =>
                    {
                        View.ShowAuthorizationErrors(errors);
                    };
                    DevicesManager.AuthorizeDevice(AuthPendingDevice, callback);
                }).Start();
            else View.NotifyErrorsInFields();
        }
    }
}
