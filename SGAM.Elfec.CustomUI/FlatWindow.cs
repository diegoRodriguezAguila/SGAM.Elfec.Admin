using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;


namespace SGAM.Elfec.CustomUI
{
    public class FlatWindow : Window
    {
        #region FlatWindow Properties
        #region FlatWindow Property : MinimizeButtonVisibility
        public static readonly DependencyProperty MinimizeButtonVisibilityProperty = DependencyProperty.RegisterAttached(
          "MinimizeButtonVisibility",
          typeof(Visibility),
          typeof(FlatWindow),
          new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        public static void SetMinimizeButtonVisibility(UIElement element, Visibility value)
        {
            element.SetValue(MinimizeButtonVisibilityProperty, value);
        }
        public static Visibility GetMinimizeButtonVisibility(UIElement element)
        {
            return (Visibility)element.GetValue(MinimizeButtonVisibilityProperty);
        }
        public Visibility MinimizeButtonVisibility
        {
            get { return (Visibility)GetValue(MinimizeButtonVisibilityProperty); }
            set { SetValue(MinimizeButtonVisibilityProperty, value); }
        }
        #endregion
        #region FlatWindow Property : MaxResButtonVisibility
        public static readonly DependencyProperty MaxResButtonVisibilityProperty = DependencyProperty.RegisterAttached(
          "MaxResButtonVisibility",
          typeof(Visibility),
          typeof(FlatWindow),
          new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        public static void SetMaxResButtonVisibility(UIElement element, Visibility value)
        {
            element.SetValue(MaxResButtonVisibilityProperty, value);
        }
        public static Visibility GetMaxResButtonVisibility(UIElement element)
        {
            return (Visibility)element.GetValue(MaxResButtonVisibilityProperty);
        }
        public Visibility MaxResButtonVisibility
        {
            get { return (Visibility)GetValue(MaxResButtonVisibilityProperty); }
            set { SetValue(MaxResButtonVisibilityProperty, value); }
        }
        #endregion
        #endregion

        static FlatWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatWindow),
                new FrameworkPropertyMetadata(typeof(FlatWindow)));
        }

        public override void OnApplyTemplate()
        {
            Button minimizeButton = (Button)GetTemplateChild("MinimizeButton");
            if (minimizeButton != null)
                minimizeButton.Click += MinimizeClick;

            Button restoreButton = (Button)GetTemplateChild("MaxResButton");
            if (restoreButton != null)
                restoreButton.Click += RestoreClick;

            Button closeButton = (Button)GetTemplateChild("CloseButton");
            if (closeButton != null)
                closeButton.Click += CloseClick;
            Grid titleBar = (Grid)GetTemplateChild("TitleBar");
            titleBar.MouseLeftButtonDown += (o, e) => DragMove();
            base.OnApplyTemplate();
        }

        protected void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected void RestoreClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.MaxWidth = SystemParameters.WorkArea.Width+12;
                this.MaxHeight = SystemParameters.WorkArea.Height+12;
            }
            WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        protected void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
