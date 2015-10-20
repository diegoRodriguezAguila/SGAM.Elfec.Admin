using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class AddNewApplicationPresenter : BasePresenter<IAddNewApplicationView>
    {
        public AddNewApplicationPresenter(IAddNewApplicationView view) : base(view)
        {
            NewApplication = new Application();
        }

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
                    NewApplication = ApkManager.GetApplication(ApkPath);
                }
            ).Start();
        }

        private void AddNewApplication()
        {

        }
        #endregion
    }
}
