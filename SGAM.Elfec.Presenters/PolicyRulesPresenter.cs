using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Policy> _policies;
        private Policy _selectedPolicy;
        #endregion
        #region Properties
        public ObservableCollection<Policy> Policies
        {
            get { return _policies; }
            set
            {
                _policies = value;
                SelectedPolicy = _policies.FirstOrDefault();
                RaisePropertyChanged("Policies");
            }
        }

        public Policy SelectedPolicy
        {
            get { return _selectedPolicy; }
            set
            {
                _selectedPolicy = value;
                RaisePropertyChanged("SelectedPolicy");
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
                //View.OnLoadingData(isRefresh);
                var callback = new ResultCallback<IList<Policy>>();
                callback.Success += (s, policies) =>
                {
                    Policies = new ObservableCollection<Policy>(policies);
                    //View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    //View.OnLoadingErrors(isRefresh, errors);
                };
                PolicyManager.GetAllPolicies(callback);
            }).Start();
        }
        #endregion
    }
}
