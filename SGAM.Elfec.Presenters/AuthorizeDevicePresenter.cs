﻿using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    public class AuthorizeDevicePresenter : BasePresenter<IAuthorizeDeviceView>
    {
        public Device AuthPendingDevice { get; set; }

        public AuthorizeDevicePresenter(IAuthorizeDeviceView view, Device authPendingDevice) : base(view)
        {
            AuthPendingDevice = authPendingDevice;
        }

        public void AuthorizeDevice()
        {
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
        }
    }
}