using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class AddNewApplicationPresenter : BasePresenter<IAddNewApplicationView>
    {
        public AddNewApplicationPresenter(IAddNewApplicationView view) : base(view) { }

        #region Private Attributes
        private string _apkPath;
        private Application _newApplication;
        #endregion

        #region Properties
        public string ApkPath
        {
            get { return _apkPath; }
            set
            {
                _apkPath = value;
                RaisePropertyChanged("ApkPath");
                LoadApkInfo();
            }
        }

        public Application NewApplication
        {
            get { return _newApplication; }
            set
            {
                _newApplication = value;
                RaisePropertyChanged("NewApplication");
            }
        }

        public ICommand AddNewApplicationCommand { get { return new DelegateCommand(AddNewApplication); } }

        #endregion

        #region Private Methods
        /// <summary>
        /// Carga la información del apk seleccionado en ApkPath
        /// </summary>
        private void LoadApkInfo()
        {
            new Thread(() =>
                {
                    View.ShowLoadingAPK();
                    NewApplication = null;
                    var callback = new ResultCallback<Application>();
                    callback.Success += (s, app) =>
                    {
                        NewApplication = app;
                        View.OnAPKLoadFinished();
                    };
                    callback.Failure += (s, errors) =>
                    {
                        View.ShowAPKLoadErrors(errors);
                    };
                    new ApkManager(ApkPath).GetApplication(callback);
                }
            ).Start();
        }

        private void AddNewApplication()
        {

        }
        #endregion
    }
}
