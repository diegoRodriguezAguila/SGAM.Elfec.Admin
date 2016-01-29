using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;

namespace SGAM.Elfec.BusinessLogic
{
    public static class ApplicationsManager
    {
        public static ICollection<int> RegisterApplication(Application newApplication)
        {
            List<int> dasd = new List<int>();
            return null;
        }

        /// <summary>
        /// Obtiene todas las aplicaciones, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAllApplications(ResultCallback<IList<Application>> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,name";
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IApplicationsEndpoint>(user.Username, user.AuthenticationToken).GetAllApplications(parameters));
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
    }
}
