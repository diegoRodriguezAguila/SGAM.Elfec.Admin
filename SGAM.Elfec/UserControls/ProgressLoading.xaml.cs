using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.UserControls
{
    /// <summary>
    /// Interaction logic for ProgressLoading.xaml
    /// </summary>
    public partial class ProgressLoading : UserControl
    {
        public ProgressLoading()
        {
            InitializeComponent();
            DataContext = this;
        }
        #region Properties
        #region Message

        public static DependencyProperty MessageProperty = DependencyProperty.RegisterAttached(
                "Message",
                typeof(string),
                typeof(ProgressLoading));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion
        #endregion
    }
}
