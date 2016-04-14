using Fluent;
using SGAM.Elfec.Interfaces;

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
            Owner = System.Windows.Application.Current.MainWindow;
        }
    }
}
