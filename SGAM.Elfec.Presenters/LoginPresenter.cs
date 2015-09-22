using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.Security;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    /// <summary>
    /// Presenter para la vista de login
    /// </summary>
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        public LoginPresenter(ILoginView view) : base(view)
        {
        }

        /// <summary>
        /// Inicia el proceso de Login
        /// </summary>
        public void LogIn()
        {
            string username = View.Username;
            string password = View.Password;
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
                SessionManager.Instance.LogIn(username, password, callback);
            }).Start();
        }
    }
}
