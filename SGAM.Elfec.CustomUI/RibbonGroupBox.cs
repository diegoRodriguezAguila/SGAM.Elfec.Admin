using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.CustomUI
{
    public class RibbonGroupBox : GroupBox
    {
        #region RibbonGroupBox Property : AccessButtonVisibility
        public static readonly DependencyProperty AccessButtonVisibilityProperty = DependencyProperty.RegisterAttached(
          "AccessButtonVisibility",
          typeof(Visibility),
          typeof(RibbonGroupBox),
          new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        public static void SetAccessButtonVisibility(UIElement element, Visibility value)
        {
            element.SetValue(AccessButtonVisibilityProperty, value);
        }
        public static Visibility GetAccessButtonVisibility(UIElement element)
        {
            return (Visibility)element.GetValue(AccessButtonVisibilityProperty);
        }
        public Visibility AccessButtonVisibility
        {
            get { return (Visibility)GetValue(AccessButtonVisibilityProperty); }
            set { SetValue(AccessButtonVisibilityProperty, value); }
        }
        #endregion
        #region RibbonGroupBox Event: AccesButtonClick
        public static readonly RoutedEvent AccesButtonClickEvent = EventManager.RegisterRoutedEvent(
        "AccesButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RibbonGroupBox));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler AccesButtonClick
        {
            add { AddHandler(AccesButtonClickEvent, value); }
            remove { RemoveHandler(AccesButtonClickEvent, value); }
        }
        #endregion
        static RibbonGroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonGroupBox),
                new FrameworkPropertyMetadata(typeof(RibbonGroupBox)));
        }

        private void RaiseAccessButtonClick(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(AccesButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        public override void OnApplyTemplate()
        {
            Button accessButton = (Button)GetTemplateChild("AccessButton");
            if (accessButton != null)
                accessButton.Click += RaiseAccessButtonClick;
            base.OnApplyTemplate();
        }
    }
}
