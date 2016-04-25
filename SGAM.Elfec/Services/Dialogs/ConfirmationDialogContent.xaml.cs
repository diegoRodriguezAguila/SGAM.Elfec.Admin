using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.Services.Dialogs
{
    /// <summary>
    /// Interaction logic for ConfirmationDialogContent.xaml
    /// </summary>
    public partial class ConfirmationDialogContent : UserControl
    {
        internal ConfirmationDialogContent()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties
        #region ConfirmationContent

        public static DependencyProperty ConfirmationContentProperty = DependencyProperty.RegisterAttached(
                "ConfirmationContent",
                typeof(UIElement),
                typeof(ConfirmationDialogContent));
        public UIElement ConfirmationContent
        {
            get { return (UIElement)GetValue(ConfirmationContentProperty); }
            set { SetValue(ConfirmationContentProperty, value); }
        }
        #endregion

        #region Message

        public static DependencyProperty MessageProperty = DependencyProperty.RegisterAttached(
                "Message",
                typeof(string),
                typeof(ConfirmationDialogContent));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion
        #endregion
    }
}
