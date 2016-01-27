using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddNewUserGroup.xaml
    /// </summary>
    public partial class AddNewUserGroup : UserControl, IAddNewUserGroupView
    {
        public AddNewUserGroup()
        {
            InitializeComponent();
            var presenter = new AddNewUserGroupPresenter(this);
            UserSelector.ItemFilter += presenter.FilterUsers;
            DataContext = presenter;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
            MainWindowService.Instance.MainWindowView.GoBack();
        }

        #region Interface methods

        public void ShowRegisteringUserGroup()
        {
            throw new NotImplementedException();
        }

        public void ShowRegistrationErrors(params Exception[] errors)
        {
            throw new NotImplementedException();
        }

        public void ShowUserGroupRegistered(UserGroup userGroup)
        {
            throw new NotImplementedException();
        }

    }
    #endregion
}
