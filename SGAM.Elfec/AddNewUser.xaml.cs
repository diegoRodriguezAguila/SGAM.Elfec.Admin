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
            MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
            MainWindowService.Instance.MainWindowView.GoBack();
        }


        #region Interface Methods

        public void OnDataLoaded()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                if (Transitioning.Content != UserInfoPanel)
                    Transitioning.Content = UserInfoPanel;
            }));
        }

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingUsers;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgLoadingUsers);
                Transitioning.Content = _indeterminateLoading;
            }));
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                }));
            }
        }

        public void ShowRegisteringUser()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgRegisteringUser;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgRegisteringUser);
                TransitioningRegister.Content = _indeterminateLoading;
            }));
        }

        public void ShowRegistrationErrors(params Exception[] errors)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                _errorMessage.BtnOk.Click += (s, o) =>
                {
                    MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                    TransitioningRegister.Content = null;
                };
                TransitioningRegister.Content = _errorMessage;
            }));
        }

        public void ShowUserRegisteredSuccessfully(User user)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var mainWindow = MainWindowService.Instance.MainWindowView;
                mainWindow.SetStatusBarDefault();
                mainWindow.ChangeToUsersView(true);
                mainWindow.NotifyUser(Properties.Resources.TitleSuccess,
                    String.Format(Properties.Resources.MsgUserRegisteredSuccessfully, user.FirstName, user.LastName));
            }));
        }

        #endregion

        private void TxtSearch_Search(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Searcheando", "SEARCHED");
        }
    }
}
