using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;

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
        #endregion

        #region Properties
        public Application Application
        {
            get { return _application; }
            set
            {
                _application = value;
                RaisePropertyChanged("Application");
            }
        }

        #endregion
    }
}
