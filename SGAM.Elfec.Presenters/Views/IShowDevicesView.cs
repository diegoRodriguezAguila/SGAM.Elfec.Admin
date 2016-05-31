using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IShowDevicesView : ILoadingContentErrorView
    {
        void ViewDeviceAuthorization(Device device);
        void ProcessingStatusChange();
        void ErrorChangingStatus(Exception error);
        void StatusChanged();
    }
}
