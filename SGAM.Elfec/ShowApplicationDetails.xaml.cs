using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowApplicationDetails.xaml
    /// </summary>
    public partial class ShowApplicationDetails : UserControl, IShowApplicationDetailsView
    {
        public ShowApplicationDetails(Model.Application application)
        {
            InitializeComponent();
            DataContext = new ShowApplicationDetailsPresenter(this, application);
        }

        #region
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.StatusBarDefault();
            MainWindowService.Instance.MainWindowView.Back();
        }

    }
}
