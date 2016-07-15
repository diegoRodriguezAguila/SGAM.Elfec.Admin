using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Reactive.Linq;
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
            View.ShowLoadingAPK();
            NewApplication = null;
            new ApkManager(ApkPath).GetApplication()
            .ObserveOn(SynchronizationContext.Current)
            .Subscribe((app) =>
            {
                NewApplication = app;
                View.OnAPKLoadFinished();
            },
            View.ShowAPKLoadError);
        }

        /// <summary>
        /// Sube el apk seleccionado
        /// </summary>
        private void UploadApplication()
        {
            View.ShowUploadingAplication();
            var listener = new UploadProgressListener();
            listener.ProgressChanged += (s, ev) =>
            {
                UploadProgressPercentage = (int)(ev.ProgressPercentage * 1.95);
            };
            ApplicationsManager.RegisterApplication(ApkPath, listener)
            .ObserveOn(SynchronizationContext.Current)
            .Subscribe(View.ShowAplicationUploadedSuccessfully,
             View.ShowAplicationUploadError);
        }
        #endregion
    }
}
