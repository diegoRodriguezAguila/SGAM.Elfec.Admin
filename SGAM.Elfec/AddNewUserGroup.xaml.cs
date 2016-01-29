using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
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
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public AddNewUserGroup()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _errorMessage = new ErrorMessage();
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
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgRegisteringUserGroup;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgRegisteringUserGroup);
                TransitioningRegister.Content = _indeterminateLoading;
            }));
        }

        public void ShowRegistrationErrors(params Exception[] errors)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                _errorMessage.BtnOk.Click += (s, o) =>
                {
                    TransitioningRegister.Content = null;
                };
                TransitioningRegister.Content = _errorMessage;
            }));
        }

        public void ShowUserGroupRegistered(UserGroup userGroup)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindowView;
                mainWindow.SetStatusBarDefault();
                mainWindow.ChangeToUsersView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    String.Format(Properties.Resources.MsgUserRegisteredSuccessfully, userGroup.Name));
            }));
        }

    }
    #endregion
}
