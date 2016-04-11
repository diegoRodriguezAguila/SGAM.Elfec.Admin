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
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : UserControl, IAddNewUserView
    {
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public AddNewUser()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _errorMessage = new ErrorMessage();
            DataContext = new AddNewUserPresenter(this);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService.Instance.MainWindowView.StatusBarDefault();
            MainWindowService.Instance.MainWindowView.Back();
        }


        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindowView.StatusBarDefault();
                if (Transitioning.Content != UserInfoPanel)
                    Transitioning.Content = UserInfoPanel;
            });
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingUsers;
                MainWindowService.Instance.MainWindowView.StatusBar(Properties.Resources.MsgLoadingUsers);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    MainWindowService.Instance.MainWindowView.StatusBarDefault();
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        public void ShowRegisteringUser()
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgRegisteringUser;
                MainWindowService.Instance.MainWindowView.StatusBar(Properties.Resources.MsgRegisteringUser);
                TransitioningRegister.Content = _indeterminateLoading;
            });
        }

        public void ShowRegistrationErrors(params Exception[] errors)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                MainWindowService.Instance.MainWindowView.StatusBarDefault();
                _errorMessage.BtnOk.Click += (s, o) =>
                {
                    TransitioningRegister.Content = null;
                };
                TransitioningRegister.Content = _errorMessage;
            });
        }

        public void ShowUserRegisteredSuccessfully(User user)
        {
            Dispatcher.InvokeAsync(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindowView;
                mainWindow.StatusBarDefault();
                mainWindow.UsersView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    string.Format(Properties.Resources.MsgUserGroupRegisteredSuccessfully, user.FullName));
            });
        }

        #endregion
    }
}
