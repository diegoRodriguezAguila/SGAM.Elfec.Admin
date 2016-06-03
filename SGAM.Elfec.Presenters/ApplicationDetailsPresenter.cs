using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class ApplicationDetailsPresenter : BasePresenter<IApplicationDetailsView>
    {
        public ApplicationDetailsPresenter(IApplicationDetailsView view,
            Application application) : base(view)
        {
            Application = application;
            Application.AppVersions = Application.AppVersions.ToObservableCollectionAsync();
        }

        #region Private Attributes
        private Application _application;
        private AppVersion _selectedVersion;
        #endregion

        #region Properties
        public Application Application
        {
            get { return _application; }
            set
            {
                _application = value;
                if (_application != null && _application.AppVersions.Count > 0)
                    SelectedVersion = _application.AppVersions.FirstOrDefault();
                RaisePropertyChanged("Application");
            }
        }

        public AppVersion SelectedVersion
        {
            get { return _selectedVersion; }
            set
            {
                _selectedVersion = value;
                RaisePropertyChanged("SelectedVersion");
            }
        }

        #endregion
        #region Commands 

        public ICommand DisableAppVersionCommand { get { return new ListItemCommand<AppVersion>(DisableAppVersion); } }
        public ICommand EnableAppVersionCommand { get { return new ListItemCommand<AppVersion>(EnableAppVersion); } }

        #endregion

        #region Private Methods
        /// <summary>
        /// Da de alta una versión de aplicación
        /// </summary>
        /// <param name="appVersion">versión de aplicación</param>
        private void EnableAppVersion(AppVersion appVersion)
        {
            ChangeAppVersionStatus(appVersion, ApiStatus.Enabled);
        }

        /// <summary>
        /// Da de baja una versión de aplicación
        /// </summary>
        /// <param name="appVersion">versión de aplicación</param>
        private void DisableAppVersion(AppVersion appVersion)
        {
            ChangeAppVersionStatus(appVersion, ApiStatus.Disabled);
        }


        private void ChangeAppVersionStatus(AppVersion appVersion, ApiStatus status)
        {
            View.ProcessingStatusChange();
            ApplicationsManager.UpdateAppVersionStatus(Application.Package,
                appVersion.Version, status)
                .Select((updatedApp) =>
                updatedApp.AppVersions.Where(v =>
                        v.Version == appVersion.Version).FirstOrDefault())
                .ObserveOnDispatcher()
                .Subscribe(
                (updatedAppVersion) =>
                {
                    int pos = Application.AppVersions.IndexOf(appVersion);
                    Application.AppVersions.RemoveAt(pos);
                    Application.AppVersions.Insert(pos, updatedAppVersion);
                    SelectedVersion = updatedAppVersion;
                    View.StatusChanged();
                }, View.ErrorChangingStatus);
        }

        #endregion
    }
}
