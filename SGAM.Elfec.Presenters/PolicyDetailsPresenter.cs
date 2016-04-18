using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class PolicyDetailsPresenter : BasePresenter<IPolicyDetailsView>
    {
        public PolicyDetailsPresenter(IPolicyDetailsView view, Policy policy) : base(view)
        {
            Policy = policy;
        }

        #region Private Attributes
        private Policy _policy;
        #endregion

        #region Properties
        public Policy Policy
        {
            get { return _policy; }
            set
            {
                _policy = value;
                RaisePropertyChanged("Policy");
            }
        }

        #region Commands
        public ICommand AddRuleCommand { get { return new DelegateCommand(AddRule); } }
        #endregion

        #endregion

        private void AddRule()
        {

        }

    }
}
