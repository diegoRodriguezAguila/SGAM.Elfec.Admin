using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowAllApps.xaml
    /// </summary>
    public partial class ShowApplications : UserControl, IShowApplicationsView
    {
        private ShowApplicationsPresenter _presenter;

        public ShowApplications()
        {
            InitializeComponent();
            _presenter = new ShowApplicationsPresenter(this);
            DataContext = _presenter;
        }

        public void ViewAppDetail(int appId)
        {
            throw new NotImplementedException();
        }

        private void AppContextMenu_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListViewApplications.SelectedItem;
        }

        private void AppContextMenu_Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = ListViewApplications.SelectedItems;
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            throw new NotImplementedException();
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            throw new NotImplementedException();
        }

        public void OnDataLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
