using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.UserControls
{
    /// <summary>
    /// Interaction logic for GroupTitleControl.xaml
    /// </summary>
    public partial class GroupTitleControl : UserControl
    {
        public GroupTitleControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties
        #region Text

        public static DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
                "Text",
                typeof(string),
                typeof(GroupTitleControl));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion
        #endregion
    }
}
