using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.UserControls
{
    /// <summary>
    /// Interaction logic for ErrorControl.xaml
    /// </summary>
    public partial class ErrorControl : UserControl
    {
        public ErrorControl()
        {
            InitializeComponent();
            DataContext = this;
        }
        #region Properties
        #region Message

        public static DependencyProperty MessageProperty = DependencyProperty.RegisterAttached(
                "Message",
                typeof(string),
                typeof(ErrorControl));
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
                typeof(ErrorControl), 
                new PropertyMetadata(Orientation.Vertical));
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        #endregion
        #endregion
    }
}
