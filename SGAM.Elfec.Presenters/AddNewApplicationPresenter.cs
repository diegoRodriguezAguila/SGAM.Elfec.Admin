using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
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
        private Application _newApplication;
        #endregion

        #region Properties
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
        private void AddNewApplication()
        {

        }
        #endregion
    }
}
