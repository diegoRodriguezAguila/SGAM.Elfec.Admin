﻿using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace SGAM.Elfec.BusinessLogic
{
    public static class ApplicationsManager
    {

        /// <summary>
        /// Obtiene todas las aplicaciones, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAllApplications(ResultCallback<IList<Application>> callback)
        {
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,name";
            parameters["include"] = "app_versions";
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IApplicationsEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .GetAllApplications(parameters));
        }


        /// <summary>
        /// Invoca al servicio de registro de una aplicación, uploadea el apk de la aplicación
        /// con sus debidos callbacks del progreso
        /// </summary>
        /// <param name="apkPath"></param>
        /// <param name="callback"></param>
        public static async void RegisterApplication(string apkPath, UploadResultCallback<Application> callback)
        {
            try
            {
                User user = SessionManager.Instance.CurrentLoggedUser;
                var uploader = new MultipartUploader(Settings.Properties.SGAM.Default.BaseApiURL,
                    "applications", apkPath);
                uploader.Headers.Add("X-Api-Token", user.AuthenticationToken);
                uploader.Headers.Add("X-Api-Username", user.Username);
                uploader.UploadProgressChanged += callback.OnUploadProgressChanged;
                var app = await uploader.UploadAsync<Application>();
                callback.OnSuccess(uploader, app);
            }
            catch (Exception e)
            {
                callback.AddErrors(RestErrorInterpreter.InterpretWebServiceError(e));
                callback.OnFailure(null);
            }
        }

        /// <summary>
        /// Updates the app version status of an app
        /// </summary>
        /// <param name="package">package of an app</param>
        /// <param name="version">version of an app</param>
        /// <param name="newStatus">status to update</param>
        /// <returns>Observable of an app</returns>
        public static IObservable<Application> UpdateAppVersionStatus(string package,
            string version, ApiStatus newStatus)
        {
            return RestEndpointFactory.Create<IApplicationsEndpoint>(
                SessionManager.Instance.CurrentLoggedUser)
                .UpdateAppVersionStatus(package, version, new AppVersion { Status = newStatus })
                .ToObservable()
                .SubscribeOn(TaskPoolScheduler.Default)
                .InterpretingErrors();
        }
    }
}
