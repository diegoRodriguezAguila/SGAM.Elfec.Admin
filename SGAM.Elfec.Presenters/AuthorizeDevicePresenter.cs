using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System;
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
            if (!AuthPendingDevice.IsValid)
            {
                View.NotifyErrorsInFields();
                return;
            }
            View.ShowProcesingAuthorization();
            DevicesManager.AuthorizeDevice(AuthPendingDevice)
                .Subscribe(View.ShowDeviceAuthorizedSuccessfuly,
                View.ShowAuthorizationError);
        }
    }
}
