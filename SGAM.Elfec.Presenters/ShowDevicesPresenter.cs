using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
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

        public ICommand AuthorizeDeviceCommand
        {
            get
            {
                return new ListItemCommand<Device>((device) =>
                {
                    View.ViewDeviceAuthorization(device);
                });
            }
        }
        public ICommand EnableDeviceCommand { get { return new ListItemCommand<Device>(EnableDevice); } }


        public ICommand DisableDeviceCommand { get { return new ListItemCommand<Device>(DisableDevice); } }
        #endregion

        #region Public Methods

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
                    Devices = devices.ToObservableCollectionAsync();
                    View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    View.OnLoadingErrors(isRefresh, errors);
                };
                DevicesManager.GetAllDevices(callback);
            }).Start();
        }

        #endregion Public Methods

        #region Private Methods

        private void EnableDevice(Device device)
        {
            View.ProcessingStatusChange();
            DevicesManager.UpdateDeviceStatus(device.Imei, DeviceStatus.Authorized)
                .ObserveOnDispatcher()
                .Subscribe(
                (updatedDevice) =>
                {
                    RefreshUpatedStatus(device, updatedDevice);
                    View.StatusChanged();
                }, View.ErrorChangingStatus);
        }

        private void DisableDevice(Device device)
        {
            View.ProcessingStatusChange();
            DevicesManager.UpdateDeviceStatus(device.Imei, DeviceStatus.Unauthorized)
                .ObserveOnDispatcher()
                .Subscribe(
                (updatedDevice) =>
                {
                    RefreshUpatedStatus(device, updatedDevice);
                    View.StatusChanged();
                }, View.ErrorChangingStatus);
        }

        private void RefreshUpatedStatus(Device device, Device updatedDevice)
        {
            //TODO look for Move instead of all this stuff, with binary search
            Devices.Remove(device);
            Devices.Add(updatedDevice);
            Devices = Devices.OrderByDescending(u => u.Status)
                .ThenBy(u => u.Name).ToObservableCollectionAsync();
        }
        #endregion
    }
}
