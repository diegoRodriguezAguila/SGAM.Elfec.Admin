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
        private int _uploadProgressPercentage;
        #endregion

        #region Properties
        public string ApkPath
        {
            get { return _apkPath; }
            set
            {
                if (value != _apkPath)
                {
                    _apkPath = value;
                    RaisePropertyChanged("ApkPath");
                    LoadApkInfo();
                }
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

        public int UploadProgressPercentage
        {
            get { return _uploadProgressPercentage; }
            set
            {
                if (value != _uploadProgressPercentage)
                {
                    _uploadProgressPercentage = value;
                    RaisePropertyChanged("UploadProgressPercentage");
                }
            }
        }

        public ICommand UploadApplicationCommand { get { return new DelegateCommand(UploadApplication); } }

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

        /// <summary>
        /// Sube el apk seleccionado
        /// </summary>
        private void UploadApplication()
        {
            new Thread(() =>
            {
                View.ShowUploadingAplication();
                var callback = new UploadResultCallback<Application>();
                callback.UploadProgressChanged += (s, ev) =>
                {
                    UploadProgressPercentage = (int)(ev.ProgressPercentage*1.8);
                };
                callback.Success += (s, app) =>
                {
                    View.ShowAplicationUploadedSuccessfully(app);
                };
                callback.Failure += (s, errors) =>
                {
                    View.ShowAplicationUploadErrors(errors);
                };
                ApplicationsManager.RegisterApplication(ApkPath, callback);
            }
            ).Start();
        }
        #endregion
    }
}
