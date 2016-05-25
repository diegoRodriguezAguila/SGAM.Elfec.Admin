using RestEase;
using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Exceptions;
using SGAM.Elfec.WebServices.LocalStorage;
using System;
using System.Net.Http;

namespace SGAM.Elfec.Security
{
    public class SessionManager : ObservableObject
    {
        private static WeakReference<SessionManager> _sessionManagerInstanceRef;

        /// <summary>
        /// No se puede instanciar esta clase directamente, debe utilizar la propiedad <see cref="Instance"/>
        /// </summary>
        private SessionManager()
        {
        }

        static SessionManager()
        {
            _sessionManagerInstanceRef = new WeakReference<SessionManager>(null);
        }

        public static SessionManager Instance
        {
            get
            {
                SessionManager currentSessionManager;
                if (!_sessionManagerInstanceRef.TryGetTarget(out currentSessionManager))
                {
                    currentSessionManager = new SessionManager();
                    _sessionManagerInstanceRef.SetTarget(currentSessionManager);
                }
                return currentSessionManager;
            }
        }
        /// <summary>
        /// Inicia el proceso de login del usuario
        /// </summary>
        /// <param name="username">nombre de usuario</param>
        /// <param name="password">contraseña</param>
        /// <param name="callback">callback para eventos de errores y exito</param>
        public async void LogIn(string username, string password, ResultCallback<User> callback = null)
        {
            if (callback == null)
                callback = new ResultCallback<User>();
            try
            {
                var user = await new ServerTokenAuth().LogInAsync(username, password);
                if (user != null)
                {
                    if (PermissionManager.Instance.HasAdminAccessPermission(user))
                    {
                        UserDAL.CurrentUser = AuthTokenProtect.ProtectToken(user);
                        RaisePropertyChanged("CurrentLoggedUser");
                        callback.OnSuccess(this, user);
                        return;
                    }
                    else callback.AddErrors(new PermissionException(user, Permission.ADMIN_ACCESS));
                }
            }
            catch (ApiException e)
            {
                callback.AddErrors(RestErrorInterpreter.InterpretError(e));
            }
            catch (HttpRequestException)
            {
                callback.AddErrors(new ServerConnectException());
            }
            callback.OnFailure(this);
        }

        /// <summary>
        /// Es el usuario logeado actual
        /// </summary>
        public User CurrentLoggedUser
        {
            get
            {
                return AuthTokenProtect.UnprotectToken(UserDAL.CurrentUser);
            }
        }
    }
}
