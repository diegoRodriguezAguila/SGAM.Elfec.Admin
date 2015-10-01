using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    /// <summary>
    /// Presenter para la vista de mostrar dispositivos
    /// </summary>
    public class ShowDevicesPresenter : BasePresenter<IShowDevicesView>
    {
        public ShowDevicesPresenter(IShowDevicesView view) : base(view) { }

        /// <summary>
        /// Realiza la carga de todos los dipositivos, ya sea por webservices o de la caché
        /// </summary>
        public void LoadAllDevices()
        {
            new Thread(() =>
            {
                View.ShowLoading();
                var callback = new ResultCallback<IList<Device>>();
                callback.Success += (s, devices) =>
                {
                    View.ShowContentData(devices);
                };
                callback.Failure += (s, errors) =>
                {
                    View.ShowErrors(false, errors);
                };
                DevicesManager.GetAllDevices(callback);
            }).Start();
        }
    }
}
