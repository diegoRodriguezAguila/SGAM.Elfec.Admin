using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.WebServices;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Security;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    /// <summary>
    /// Presenter para la vista de login
    /// </summary>
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        public LoginPresenter(ILoginView view) : base(view)
        {
            RemoteSession = new RemoteSession();
        }

        #region Private Attributes
        private RemoteSession _remoteSession;
        #endregion

        #region Properties

        public RemoteSession RemoteSession
        {
            get { return _remoteSession; }
            set
            {
                _remoteSession = value;
                RaisePropertyChanged("RemoteSession");
            }
        }

        public ICommand LogInCommand { get { return new DelegateCommand(LogIn); } }

        #endregion
        /// <summary>
        /// Inicia el proceso de Login
        /// </summary>
        private void LogIn()
        {
            if (RemoteSession.IsValid)
                new Thread(() =>
                {
                    View.ShowWaiting();
                    var callback = new ResultCallback<User>();
                    callback.Success += (s, user) =>
                    {
                        View.HideWaiting();
                        View.NotifySuccessfulLogin(user);
                    };
                    callback.Failure += (s, errors) =>
                    {
                        View.ShowLoginErrors(errors);
                    };
                    SessionManager.Instance.LogIn(RemoteSession.Username, RemoteSession.Password, callback);
                }).Start();
        }
    }
}
