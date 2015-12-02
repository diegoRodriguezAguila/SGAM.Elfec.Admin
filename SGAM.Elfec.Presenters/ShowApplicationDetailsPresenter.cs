using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System.Linq;

namespace SGAM.Elfec.Presenters
{
    public class ShowApplicationDetailsPresenter : BasePresenter<IShowApplicationDetailsView>
    {
        public ShowApplicationDetailsPresenter(IShowApplicationDetailsView view,
            Application application) : base(view)
        {
            Application = application;
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
    }
}
