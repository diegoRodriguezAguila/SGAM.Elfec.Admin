using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class ShowApplicationsPresenter : BasePresenter<IShowApplicationsView>
    {
        public ShowApplicationsPresenter(IShowApplicationsView view) : base(view)
        {
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
        public ICommand ShowApplicationDetailsCommand
        {
            get
            {
                return new ListItemCommand<Application>((app) =>
                {
                    View.ShowApplicationDetails(app);
                });
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
            View.OnLoadingData(isRefresh);
            ApplicationsManager.GetAllApplications()
            .ObserveOn(SynchronizationContext.Current)
            .Subscribe((apps) =>
            {
                Applications = new ObservableCollection<Application>(apps);
                View.OnDataLoaded();
            }, (e) => View.OnLoadingErrors(isRefresh, e));
        }
        #endregion
    }
}
