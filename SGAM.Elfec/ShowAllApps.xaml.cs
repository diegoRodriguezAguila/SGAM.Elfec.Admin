using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowAllApps.xaml
    /// </summary>
    public partial class ShowAllApps : UserControl, IShowAllAppsView
    {
        private ShowAllAppsPresenter _presenter;
        public ShowAllApps()
        {
            InitializeComponent();
            _presenter = new ShowAllAppsPresenter(this);
            this.DataContext = _presenter;
        }

        public void ViewAppDetail(int appId)
        {
            throw new NotImplementedException();
        }

        private void AppContextMenu_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = LVAllApps.SelectedItem;
        }

        private void AppContextMenu_Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LVAllApps.SelectedItems;
        }
    }
}
