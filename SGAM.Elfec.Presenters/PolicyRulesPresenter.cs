using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Presentation;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Linq;
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
                Selected = _policiesRoot.Policies.FirstOrDefault();
                RaisePropertyChanged("PoliciesRoot");
            }
        }

        public object Selected
        {
            get { return _selectedPolicy != null ? (object)_selectedPolicy : (object)_selectedRule; }
            set
            {
                _selectedPolicy = value as Policy;
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
            new Thread(() =>
            {
                View.OnLoadingData(isRefresh);
                var callback = new ResultCallback<IList<Policy>>();
                callback.Success += (s, policies) =>
                {
                    PoliciesRoot = new PoliciesThreeViewRoot(policies);
                    View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    View.OnLoadingErrors(isRefresh, errors);
                };
                PolicyManager.GetAllPolicies(callback);
            }).Start();
        }
        #endregion
    }
}
