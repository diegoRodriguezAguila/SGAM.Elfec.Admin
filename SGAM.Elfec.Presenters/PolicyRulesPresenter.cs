using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Presentation;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    public class PolicyRulesPresenter : BasePresenter<IPolicyRulesView>
    {
        public PolicyRulesPresenter(IPolicyRulesView view)
            : base(view)
        {
            LoadPolicies();
        }

        #region Private Attributes
        private PolicyDetailsPresenter _policyDetails;
        private PoliciesThreeViewRoot _policiesRoot;
        private Policy _selectedPolicy;
        private Rule _selectedRule;
        #endregion
        #region Properties

        public PoliciesThreeViewRoot PoliciesRoot
        {
            get { return _policiesRoot; }
            set
            {
                _policiesRoot = value;
                Selected = _policiesRoot;
                RaisePropertyChanged("PoliciesRoot");
            }
        }

        public PolicyDetailsPresenter PolicyDetails
        {
            get { return _policyDetails; }
            set
            {
                _policyDetails = value;
                RaisePropertyChanged("PolicyDetails");
            }
        }

        public object Selected
        {
            get
            {
                return _selectedPolicy != null ?
                  (object)_selectedPolicy :
                  (_selectedRule != null ? (object)_selectedRule : (object)_policiesRoot);
            }
            set
            {
                _selectedPolicy = value as Policy;
                if (_selectedPolicy != null)
                {
                    PolicyDetails.Policy = _selectedPolicy;
                    View.ShowPolicyDetails();
                }
                _selectedRule = value as Rule;
                RaisePropertyChanged("Selected");
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Carga todos los usuarios aun no registrados para poder agregarlos
        /// </summary>
        /// <param name="isRefresh"></param>
        private void LoadPolicies(bool isRefresh = false)
        {
            View.OnLoadingData(isRefresh);
            PolicyManager.GetAllPolicies()
            .ObserveOn(SynchronizationContext.Current)
                .Subscribe((policies) =>
                {
                    PoliciesRoot = new PoliciesThreeViewRoot(policies);
                    View.OnDataLoaded();
                }, (e) => View.OnLoadingErrors(false, e));
        }
        #endregion
    }
}
