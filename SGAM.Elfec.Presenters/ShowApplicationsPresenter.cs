﻿using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    public class ShowApplicationsPresenter : ObservableObject
    {
        public IShowApplicationsView View { get; set; }
        public ShowApplicationsPresenter(IShowApplicationsView view)
        {
            this.View = view;
            LoadAllApplications();
        }
        #region Private Attributes
        private ObservableCollection<Application> _applications;
        #endregion

        #region Properties
        public ObservableCollection<Application> Applications
        {
            get { return _applications; }
            set
            {
                _applications = value;
                RaisePropertyChanged("Applications");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Realiza la carga de todas las aplicaciones, ya sea por webservices o de la caché
        /// </summary>
        /// <param name="isRefresh"></param>
        public void LoadAllApplications(bool isRefresh = false)
        {
            new Thread(() =>
            {
                View.OnLoadingData(isRefresh);
                var callback = new ResultCallback<IList<Application>>();
                callback.Success += (s, apps) =>
                {
                    Applications = new ObservableCollection<Application>(apps);
                    View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    View.OnLoadingErrors(isRefresh, errors);
                };
                ApplicationsManager.GetAllApplications(callback);
            }).Start();
        }
        #endregion
    }
}
