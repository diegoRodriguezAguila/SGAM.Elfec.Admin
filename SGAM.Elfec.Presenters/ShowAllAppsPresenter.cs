using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    public class ShowAllAppsPresenter : ObservableObject
    {
        public IShowAllAppsView View { get; set; }
        public ShowAllAppsPresenter(IShowAllAppsView view)
        {
            this.View = view;
            LoadAllApplications();
        }
        #region Observable Properties
        #region Property : AllApps
        /// <summary>
        /// The <see cref="AllApps" /> property's name.
        /// </summary>
        public const string AllAppsPropertyName = "AllApps";

        private ObservableCollection<MobileApplication> _allApps;

        /// <summary>
        /// Sets and gets the AllApps property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<MobileApplication> AllApps
        {
            get
            {
                return _allApps;
            }

            set
            {
                if (_allApps == value)
                {
                    return;
                }

                _allApps = value;
                RaisePropertyChanged(AllAppsPropertyName);
            }
        }
        #endregion
        #endregion

        #region Public Methods
        public void LoadAllApplications()
        {
            Thread loadInThread = new Thread(new ThreadStart(
                () => 
                {
                    AllApps = new ObservableCollection<MobileApplication>(MobileApplicationManager.GetAllApplications());
                }
                ));
            loadInThread.Start();
        }
        #endregion
    }
}
