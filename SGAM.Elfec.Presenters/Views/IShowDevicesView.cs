using SGAM.Elfec.Model;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IShowDevicesView : ILoadingContentErrorView
    {
        void ShowAuthorizeDevice(Device device);
    }
}
