using SGAM.Elfec.CustomUI;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using SGAM.Elfec.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application = System.Windows.Application;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddRule.xaml
    /// </summary>
    public partial class AddRule : UserControl, IAddRuleView
    {

        private SelectableTextBlock _errorMessage;
        private LoadingControl _loadingControl;
        public AddRule(Policy policy, Rule rule = null)
        {
            InitializeComponent();
            TxtValue.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            TxtException.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            var presenter = new AddRulePresenter(this, policy, rule);
            EntitySelector.ItemFilter += presenter.FilterEntities;
            DataContext = presenter;
            InitializeErrorMessage();
            _loadingControl = new LoadingControl { Orientation = Orientation.Vertical };
            Loaded += (s, e) => { ValidationErrorsAssistant.ClearErrors(RootPanel); };
        }

        private void InitializeErrorMessage()
        {
            _errorMessage = new SelectableTextBlock
            {
                FontSize = 14,
                Foreground = Application.Current.Resources["TextErrorColorBrush"] as Brush,
                TextWrapping = TextWrapping.Wrap
            };
        }

        #region Events
        public static readonly RoutedEvent AddRuleSuccessEvent =
                EventManager.RegisterRoutedEvent(
        "AddRuleSuccess",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(AddRule));

        /// <summary>
        /// Evento que se ejecuta cuando la regla fue añadida exitosamente
        /// </summary>
        public event RoutedEventHandler AddRuleSuccess
        {
            add { AddHandler(AddRuleSuccessEvent, value); }
            remove { RemoveHandler(AddRuleSuccessEvent, value); }
        }

        private void OnAddRuleSuccess(object sender)
        {
            RaiseEvent(new RoutedEventArgs(AddRuleSuccessEvent, sender));
        }
        #endregion


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
            Dispatcher.InvokeAsync(() =>
            {
                _loadingControl.Message = Properties.Resources.MsgRegisteringRule;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgRegisteringRule);
                Transitioning.Content = _loadingControl;
            });
        }

        public void Success(Rule rule)
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow
                .StatusBarDefault()
                .NotifyUser(Properties.Resources.TitleSuccess,
                    string.Format(
                        Properties.Resources.MsgRuleRegisteredSuccessfuly, rule.Name));
                OnAddRuleSuccess(this);
            });
        }
        public void Error(Exception error)
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                _errorMessage.Text = error.Message;
                Transitioning.Content = _errorMessage;
            });
        }

        #endregion
    }
}
