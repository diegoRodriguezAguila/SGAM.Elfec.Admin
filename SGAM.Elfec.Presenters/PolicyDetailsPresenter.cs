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

        public ICommand AddRuleCommand => new DelegateCommand(AddRule);
        public ICommand EditRuleCommand => new ListItemCommand<IList>(EditRule);
        public ICommand DeleteRuleCommand => new ListItemCommand<IList>(DeleteRules);

        #endregion

        #endregion

        private void AddRule()
        {
            View.AddRule(Policy);
        }

        private void EditRule(IList items)
        {
            var rule = items.Cast<Rule>().ToArray().FirstOrDefault();
            if (rule != null)
                View.EditRule(Policy, rule);
        }

        /// <summary>
        /// Deletes the selected entities from the rule
        /// </summary>
        /// <param name="selectedRules"></param>
        private void DeleteRules(IList selectedRules)
        {
            var rulesToDel = selectedRules.Cast<Rule>().ToArray();
            if (rulesToDel.Length == 0 ||
                View.DeleteConfirmation()) return;
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