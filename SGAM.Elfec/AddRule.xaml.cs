using SGAM.Elfec.CustomUI;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddRule.xaml
    /// </summary>
    public partial class AddRule : UserControl, IAddRuleView
    {
        private SelectableTextBlock _errorMessage;
        public AddRule(Policy policy, Rule rule = null)
        {
            InitializeComponent();
            TxtValue.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            TxtException.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            var presenter = new AddRulePresenter(this, policy, rule);
            EntitySelector.ItemFilter += presenter.FilterEntities;
            DataContext = presenter;
            InitializeErrorMessage();
            Loaded += (s, e) => { ValidationErrorsAssistant.ClearErrors(RootPanel); };
        }

        private void InitializeErrorMessage()
        {
            _errorMessage = new SelectableTextBlock();
            _errorMessage.FontSize = 14;
            _errorMessage.Foreground = App.Current.Resources["TextErrorColorBrush"] as Brush;
            _errorMessage.TextWrapping = TextWrapping.Wrap;
        }

        #region Interface Methods
        public void Validate()
        {
            ValidationErrorsAssistant.UpdateSources(RootPanel);
        }

        public void NotifyErrorsInFields()
        {
            _errorMessage.Text = Properties.Resources.MsgErrorsInFields;
            Transitioning.Content = _errorMessage;
        }

        public void ProcessingData()
        {
            throw new NotImplementedException();
        }

        public void Success(Rule data)
        {
            throw new NotImplementedException();
        }
        public void Error(Exception error)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
