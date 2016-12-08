using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SGAM.Elfec.UserControls
{
    /// <summary>
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        private Cursor _cursor;

        public SearchPanel()
        {
            InitializeComponent();
            DataContext = this;
            BindSearchEvents();
        }

        #region Properties

        #region SearchQuery

        public static DependencyProperty SearchQueryProperty = DependencyProperty.RegisterAttached(
            "SearchQuery",
            typeof(string),
            typeof(SearchPanel));

        public string SearchQuery
        {
            get { return (string)GetValue(SearchQueryProperty); }
            set { SetValue(SearchQueryProperty, value); }
        }

        #endregion

        #region IsOpened

        public static DependencyProperty IsOpenedProperty = DependencyProperty.RegisterAttached(
            "IsOpened",
            typeof(bool),
            typeof(SearchPanel), new PropertyMetadata(true));


        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set
            {
                if (value == false)
                    RaiseEvent(new RoutedEventArgs(ClosedEvent));
                SetValue(IsOpenedProperty, value);
            }
        }

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent ClosedEvent =
            EventManager.RegisterRoutedEvent(
                "Closed",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchPanel));

        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchPanel));


        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        #endregion

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double width = double.IsNaN(SizableContent.Width) ? SizableContent.ActualWidth : SizableContent.Width;
            double xAdjust = width + e.HorizontalChange;
            if (xAdjust < SizableContent.MinWidth)
                xAdjust = SizableContent.MinWidth;
            if (xAdjust > SizableContent.MaxWidth)
                xAdjust = SizableContent.MaxWidth;
            SizableContent.Width = xAdjust;
        }

        private void OnResizeThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            _cursor = Cursor;
            Cursor = Cursors.SizeWE;
        }

        private void OnResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            Cursor = _cursor;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            IsOpened = false;
        }

        private void BindSearchEvents()
        {
            TxtSearch.Search += (sender, args) => { RaiseEvent(new RoutedEventArgs(SearchEvent)); };
        }
    }
}