using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowAllDeviceGroups.xaml
    /// </summary>
    public partial class ShowUsers : UserControl, IShowUsersView
    {
        public ShowUsers()
        {
            InitializeComponent();
            DataContext = new ShowUsersPresenter(this);
        }

        #region Interface Methods

        public void OnDataLoaded()
        {
            throw new NotImplementedException();
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            throw new NotImplementedException();
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
