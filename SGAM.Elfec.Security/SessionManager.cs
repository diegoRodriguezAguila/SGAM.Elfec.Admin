﻿using RestEase;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Exceptions;
using SGAM.Elfec.WebServices;
using System;

using System.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace SGAM.Elfec.Security
{
    public class SessionManager
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
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="callback"></param>
        public async void LogIn(string username, string password, ResultCallback<User> callback = null)
        {
            IList<Exception> errors = new List<Exception>();
            if (callback == null)
                callback = new ResultCallback<User>();
            try
            {
                var user = await new ServerTokenAuth().LogInAsync(username, password);
                if (user != null)
                {
                    if (PermissionManager.Instance.HasAdminAccessPermission(user))
                        callback.OnSuccess(this, user);
                    else errors.Add(new PermissionException(user, Permission.ADMIN_ACCESS));
                }
            }
            catch (ApiException e)
            {
                errors.Add(RestErrorInterpreter.InterpretError(e));
            }
            catch (HttpRequestException)
            {
                errors.Add(new ServerConnectException());
            }
            if(errors.Count>0)
                callback.OnFailure(this, errors.ToArray());
        }
    }
}