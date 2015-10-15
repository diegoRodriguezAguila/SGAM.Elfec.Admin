using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    /// <summary>
    /// Presenter para la vista de mostrar dispositivos
    /// </summary>
    public class ShowDevicesPresenter : BasePresenter<IShowDevicesView>
    {
        public ShowDevicesPresenter(IShowDevicesView view) : base(view)
        {
            LoadAllDevices();
        }

        #region Private Attributes
        private ObservableCollection<Device> _devices;
        #endregion

        #region Properties
        public ObservableCollection<Device> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                RaisePropertyChanged("Devices");
            }
        }

        public ICommand AuthorizeDevice
        {
            get
            {
                return new ListItemCommand<Device>((device) =>
                {
                    View.ViewDeviceAuthorization(device);
                });
            }
        }

        #endregion


        /// <summary>
        /// Realiza la carga de todos los dipositivos, ya sea por webservices o de la caché
        /// </summary>
        public void LoadAllDevices(bool isRefresh = false)
        {
            new Thread(() =>
            {
                View.OnLoadingData(isRefresh);
                var callback = new ResultCallback<IList<Device>>();
                callback.Success += (s, devices) =>
                {
                    Devices = new ObservableCollection<Device>(devices);
                    View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    View.OnLoadingErrors(isRefresh, errors);
                };
                DevicesManager.GetAllDevices(callback);
            }).Start();
        }
    }
}
