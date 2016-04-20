using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace SGAM.Elfec.CustomUI
{
    /// <summary>
    /// Search modes foor SearchBox
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// Search mode delayed, where it fires the Search event 500ms after TextChanged
        /// </summary>
        Delayed,
        /// <summary>
        /// Search mode manual, where it fires the Search event manually on click of search button or enter
        /// </summary>
        Manual,
    }

    public class SearchBox : TextBox
    {
        #region Private Attributes
        private Button btnSearch;
        private DispatcherTimer searchEventDelayTimer;

        #endregion

        #region Dependency Properties
        #region SearchMode
        public static DependencyProperty SearchModeProperty =
            DependencyProperty.Register(
                "SearchMode",
                typeof(SearchMode),
                typeof(SearchBox),
                new PropertyMetadata(SearchMode.Delayed));
        public SearchMode SearchMode
        {
            get { return (SearchMode)GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }
        #endregion
        #region HasText
        private static DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "HasText",
                typeof(bool),
                typeof(SearchBox),
                new PropertyMetadata());
        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;
        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            set { SetValue(HasTextPropertyKey, value); }
        }
        #endregion
        #region SearchTimeDelay
        public static DependencyProperty SearchTimeDelayProperty =
    DependencyProperty.Register(
        "SearchTimeDelay",
        typeof(Duration),
        typeof(SearchBox),
        new FrameworkPropertyMetadata(
            new Duration(new TimeSpan(0, 0, 0, 0, 500)),
            new PropertyChangedCallback(OnSearchTimeDelayChanged)));

        public Duration SearchTimeDelay
        {
            get { return (Duration)GetValue(SearchTimeDelayProperty); }
            set { SetValue(SearchTimeDelayProperty, value); }
        }

        static void OnSearchTimeDelayChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SearchBox stb = o as SearchBox;
            if (stb != null)
            {
                stb.searchEventDelayTimer.Interval = ((Duration)e.NewValue).TimeSpan;
                stb.searchEventDelayTimer.Stop();
            }
        }

        #endregion
        #endregion


        #region Events
        public static readonly RoutedEvent SearchEvent =
    EventManager.RegisterRoutedEvent(
        "Search",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(SearchBox));


        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }
        #endregion

        public SearchBox() : base()
        {
            searchEventDelayTimer = new DispatcherTimer();
            searchEventDelayTimer.Interval = SearchTimeDelay.TimeSpan;
            searchEventDelayTimer.Tick += new EventHandler(OnSearchEventDelayTimerTick);

        }


        void OnSearchEventDelayTimerTick(object o, EventArgs e)
        {
            searchEventDelayTimer.Stop();
            RaiseSearchEvent();
        }


        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox),
                new FrameworkPropertyMetadata(typeof(SearchBox)));
            TextProperty.OverrideMetadata(typeof(SearchBox),
            new FrameworkPropertyMetadata(typeof(SearchBox))
            { DefaultValue = null, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (SearchMode == SearchMode.Delayed)
            {
                HasText = Text.Length != 0;
                searchEventDelayTimer.Stop();
                searchEventDelayTimer.Start();
            }
        }

        public override void OnApplyTemplate()
        {
            btnSearch = (Button)GetTemplateChild("BtnSearch");
            if (btnSearch != null)
                btnSearch.Click += SearchClick;
            base.OnApplyTemplate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape && SearchMode == SearchMode.Delayed)
                ClearText();
            else if (e.Key == Key.Return || e.Key == Key.Enter)
                RaiseSearchEvent();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            if (HasText && SearchMode == SearchMode.Delayed)
                ClearText();
            if (HasText && SearchMode == SearchMode.Manual) RaiseSearchEvent();
        }

        private void RaiseSearchEvent()
        {
            if (SearchMode == SearchMode.Manual)
                HasText = Text.Length != 0;
            RaiseEvent(new RoutedEventArgs(SearchEvent));
        }

        private void ClearText()
        {
            Text = "";
            Focus();
        }
    }
}
