using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SGAM.Elfec.CustomUI
{
    public class SelectableTextBlock : TextBox
    {
        public SelectableTextBlock()
        {
            SelectAllOnDoubleClick = true;
            MouseDoubleClick += OnDoubleClick;
        }

        #region SelectAllOnDoubleClick

        /// <summary>
        /// Gets or sets whether popup is opened
        /// </summary>
        public bool SelectAllOnDoubleClick
        {
            get { return (bool)this.GetValue(SelectAllOnDoubleClickProperty); }
            set { this.SetValue(SelectAllOnDoubleClickProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for IsOpen. 
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SelectAllOnDoubleClickProperty =
            DependencyProperty.Register("SelectAllOnDoubleClick", typeof(bool), typeof(SelectableTextBlock),
            new UIPropertyMetadata(false));

        #endregion

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectAllOnDoubleClick)
                (sender as TextBox).SelectAll();
        }
    }
}
