using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
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
        public ICommand DeleteRuleCommand { get { return new ListItemCommand<IList>(DeleteRules); } }
        #endregion

        #endregion

        private void AddRule()
        {
            View.AddRule(Policy);
        }

        /// <summary>
        /// Deletes the selected entities from the rule
        /// </summary>
        /// <param name="selectedEntities"></param>
        private void DeleteRules(IList selectedRules)
        {
            var rulesToDel = selectedRules.Cast<Rule>().ToArray();
            if (rulesToDel != null && rulesToDel.Length > 0 &&
                View.DeleteConfirmation())
            {
                View.DeletingRule();
                PolicyManager.DeleteRules(Policy.Type, rulesToDel)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe((u) =>
                    {
                        Policy.Rules.RemoveRange(rulesToDel);
                        View.RuleDeleted();
                    }, View.ErrorDeleting);
            }
        }

    }
}
