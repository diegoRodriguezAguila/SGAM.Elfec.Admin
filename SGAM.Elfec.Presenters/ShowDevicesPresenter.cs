using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
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

        private IList<Device> _allDevices;
        private ObservableCollection<Device> _devices;
        private string _searchQuery;
        private ListSearcher<Device> _listSearcher;

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

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                RaisePropertyChanged("SearchQuery");
            }
        }

        #region Commands

        public ICommand AuthorizeDeviceCommand
        {
            get { return new ListItemCommand<Device>((device) => { View.ViewDeviceAuthorization(device); }); }
        }

        public ICommand ShowDeviceDetailsCommand
        {
            get { return new ListItemCommand<Device>((device) => { View.ShowDeviceDetails(device); }); }
        }

        public ICommand EnableDeviceCommand => new ListItemCommand<Device>(EnableDevice);


        public ICommand DisableDeviceCommand => new ListItemCommand<Device>(DisableDevice);

        public ICommand SearchDevicesCommand => new DelegateCommand(SearchDevices);

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Realiza la carga de todos los dipositivos, ya sea por webservices o de la caché
        /// </summary>
        public void LoadAllDevices(bool isRefresh = false)
        {
            View.OnLoadingData(isRefresh);
            DevicesManager.GetAllDevices()
                .Subscribe((devices) =>
                {
                    _allDevices = devices;
                    Devices = devices.ToObservableCollectionAsync();
                    _listSearcher = ListSearcher<Device>.For(_allDevices)
                        .With(DeviceMatchHelper.MatchesSearchQuery);
                    _listSearcher.SearchCompleted += SearchCompleted;
                    View.OnDataLoaded();
                }, (error) => View.OnLoadingErrors(isRefresh, error));
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

        private void SearchDevices()
        {
            _listSearcher?.Search(SearchQuery);
        }

        private void SearchCompleted(object sender, IEnumerable<Device> devices)
        {
            Devices.Clear();
            Devices = devices.ToObservableCollectionAsync();
        }

        #endregion
    }
}