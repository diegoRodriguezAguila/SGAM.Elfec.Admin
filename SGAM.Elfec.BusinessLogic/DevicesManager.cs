using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Security;
using System.Collections.Generic;

namespace SGAM.Elfec.BusinessLogic
{
    public class DevicesManager
    {
        /// <summary>
        /// Obtiene todos los dipositivos, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAllDevices(ResultCallback<IList<Device>> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var restInvoker = new RestInvoker<IList<Device>>();
            restInvoker.InvokeWebService(callback, () =>
            {
                var parameters = new Dictionary<string, string>();
                parameters["order"] = "status:desc";
                return RestEndpointFactory
                    .Create<IDevicesEndpoint>(user.Username, user.AuthenticationToken).GetAllDevices(parameters);
            });
        }
    }
}
