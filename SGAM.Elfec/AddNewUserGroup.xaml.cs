using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
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

        #region Interface methods

        public void ShowRegisteringUserGroup()
        {
            Dispatcher.InvokeAsync(() =>
           {
               _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgRegisteringUserGroup;
               MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgRegisteringUserGroup);
               TransitioningRegister.Content = _indeterminateLoading;
           });
        }

        public void ShowRegistrationErrors(params Exception[] errors)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                _errorMessage.BtnOk.Click += (s, o) =>
                {
                    TransitioningRegister.Content = null;
                };
                TransitioningRegister.Content = _errorMessage;
            });
        }

        public void ShowUserGroupRegistered(UserGroup userGroup)
        {
            Dispatcher.InvokeAsync(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindow;
                mainWindow.StatusBarDefault();
                mainWindow.UsersView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    string.Format(Properties.Resources.MsgUserGroupRegisteredSuccessfully, userGroup.Name));
            });
        }

    }
    #endregion
}
