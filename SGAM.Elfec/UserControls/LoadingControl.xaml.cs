using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.UserControls
{
    /// <summary>
    /// Interaction logic for LoadingControl.xaml
    /// </summary>
    public partial class LoadingControl : UserControl
    {
        public LoadingControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties
        #region Message

        public static DependencyProperty MessageProperty = DependencyProperty.RegisterAttached(
                "Message",
                typeof(string),
                typeof(LoadingControl));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion

        #region Orientation
        public static DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(
                "Orientation",
                typeof(Orientation),
                typeof(LoadingControl), new PropertyMetadata(Orientation.Horizontal));
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        #endregion
        #endregion
    }
}
