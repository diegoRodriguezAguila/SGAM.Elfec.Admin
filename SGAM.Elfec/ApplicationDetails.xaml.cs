using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Services.Dialogs;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowApplicationDetails.xaml
    /// </summary>
    public partial class ApplicationDetails : UserControl, IApplicationDetailsView
    {
        public ApplicationDetails(Model.Application application)
        {
            InitializeComponent();
            DataContext = new ApplicationDetailsPresenter(this, application);
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

        public void ProcessingStatusChange()
        {
            MainWindowService.Instance.MainWindow.
                StatusBar(Properties.Resources.MsgUpdatingAppVersionStatus)
                .SetCursor(Cursors.AppStarting);
        }

        public void StatusChanged()
        {
            MainWindowService.Instance.MainWindow
                .StatusBarDefault()
                .DefaultCursor();
        }

        public void ErrorChangingStatus(Exception error)
        {
            MainWindowService.Instance.MainWindow
               .StatusBarDefault()
               .DefaultCursor();
            new InformationDialog
            {
                Title = Properties.Resources.TitleErrorInAppVersionStatusUpdate,
                Message = string.Format(Properties.Resources.MsgErrorInAppVersionStatusUpdate,
                error.Message),
                IconType = IconType.Warning
            }.ShowDialog();
        }
        #endregion


    }
}
