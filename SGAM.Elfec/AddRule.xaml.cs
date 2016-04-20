using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Utils;
using System;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddRule.xaml
    /// </summary>
    public partial class AddRule : UserControl, IAddRuleView
    {
        public AddRule(Policy policy, Rule rule = null)
        {
            InitializeComponent();
            TxtValue.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            TxtException.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            var presenter = new AddRulePresenter(this, policy, rule);
            EntitySelector.ItemFilter += presenter.FilterEntities;
            DataContext = presenter;
            Loaded += (s, e) => { ValidationErrorsAssistant.ClearErrors(RootPanel); };
        }

        public object AuthPanel { get; private set; }
        #region Interface Methods

        public void NotifyErrorsInFields()
        {
            //throw new NotImplementedException();
        }

        public void Validate()
        {
            ValidationErrorsAssistant.UpdateSources(RootPanel);
        }

        #endregion
    }
}
