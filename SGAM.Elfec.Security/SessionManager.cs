using RestEase;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Exceptions;
using SGAM.Elfec.WebServices;
using System;
using System.Net.Http;

namespace SGAM.Elfec.Security
{
    public class SessionManager
    {
        private static WeakReference<SessionManager> _sessionManagerInstanceRef;

        /// <summary>
        /// No se puede instanciar esta clase directamente, debe utilizar la propiedad <see cref=""/>
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

        public async void LogIn(string username, string password, ResultCallback<User> callback = null)
        {
            if (callback == null)
                callback = new ResultCallback<User>();
            try
            {
                var user = await new ServerTokenAuth().LogInAsync(username, password);
                if (user != null)
                    callback.OnSuccess(this, user);
            }
            catch (ApiException e)
            {
                callback.OnFailure(this, RestErrorInterpreter.InterpretError(e));
            }
            catch (HttpRequestException)
            {
                callback.OnFailure(this, new ServerConnectException());
            }
        }
    }
}
