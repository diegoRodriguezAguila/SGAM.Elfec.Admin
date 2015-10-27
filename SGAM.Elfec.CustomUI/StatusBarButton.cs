using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.CustomUI
{
    public class StatusBarButton : Button
    {
        public StatusBarButton()
        {
            this.Click += OnClick;
        }

        #region IsSelected

        /// <summary>
        /// Gets or sets whether popup is opened
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for IsOpen. 
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(StatusBarButton),
            new UIPropertyMetadata(false));

        #endregion

        /// <summary>
        /// Handles click event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">The event data</param>
        protected virtual void OnClick(object sender, RoutedEventArgs e)
        {
            this.IsSelected = true;
            e.Handled = false;
        }
    }
}
