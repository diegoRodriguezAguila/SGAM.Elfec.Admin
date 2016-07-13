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
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : UserControl, IAddNewUserView
    {
        private LoadingControl _indeterminateLoading;
        private ErrorControl _errorMessage;

        public AddNewUser()
        {
            InitializeComponent();
            _indeterminateLoading = new LoadingControl();
            _errorMessage = new ErrorControl();
            DataContext = new AddNewUserPresenter(this);
        }


        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.InvokeAsync(() =>
            {
                MainWindowService.Instance.MainWindow.StatusBarDefault();
                if (Transitioning.Content != UserInfoPanel)
                    Transitioning.Content = UserInfoPanel;
            });
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.Message = Properties.Resources.MsgLoadingUsers;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgLoadingUsers);
                Transitioning.Content = _indeterminateLoading;
            });
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    MainWindowService.Instance.MainWindow.StatusBarDefault();
                    _errorMessage.Message = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                });
            }
        }

        public void ShowRegisteringUser()
        {
            Dispatcher.InvokeAsync(() =>
            {
                _indeterminateLoading.Message = Properties.Resources.MsgRegisteringUser;
                MainWindowService.Instance.MainWindow.StatusBar(Properties.Resources.MsgRegisteringUser);
                TransitioningRegister.Content = _indeterminateLoading;
            });
        }

        public void ShowRegistrationError(Exception error)
        {
            Dispatcher.InvokeAsync(() =>
            {
                _errorMessage.Message = error.Message;
                MainWindowService.Instance.MainWindow.StatusBarDefault();
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
                var mainWindow = MainWindowService.Instance.MainWindow;
                mainWindow.StatusBarDefault();
                mainWindow.UsersView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    string.Format(Properties.Resources.MsgUserRegisteredSuccessfully, user.FullName));
            });
        }

        #endregion
    }
}
