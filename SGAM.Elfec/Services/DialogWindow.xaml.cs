using Fluent;
using SGAM.Elfec.Interfaces;
using System.Windows;

namespace SGAM.Elfec.Services
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : RibbonWindow, IDialog
    {
        internal DialogWindow()
        {
            InitializeComponent();
            DataContext = this;
            Owner = System.Windows.Application.Current.MainWindow;
        }

        public void SetDialogResult(bool result)
        {
            DialogResult = result;
        }

        #region Properties
        #region DialogContent

        public static DependencyProperty DialogContentProperty = DependencyProperty.RegisterAttached(
                "DialogContent",
                typeof(UIElement),
                typeof(DialogWindow));
        public UIElement DialogContent
        {
            get { return (UIElement)GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }
        #endregion
        #endregion
    }
}
