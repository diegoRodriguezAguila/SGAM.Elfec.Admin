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
                parameters["sort"] = "status:desc,name";
                return RestEndpointFactory
                    .Create<IDevicesEndpoint>(user.Username, user.AuthenticationToken).GetAllDevices(parameters);
            });
        }

        public static void AuthorizeDevice(Device deviceToAuth, ResultCallback<Device> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var restInvoker = new RestInvoker<Device>();
            restInvoker.InvokeWebService(callback, () =>
            {
                return RestEndpointFactory
                    .Create<IDevicesEndpoint>(user.Username, user.AuthenticationToken)
                    .UpdateDevice(deviceToAuth.Imei, PrepareDeviceForAuth(deviceToAuth));
            });
        }

        /// <summary>
        /// Crea una copia de solo los valores actualizables en la autorización de un dispositivo
        /// </summary>
        /// <param name="deviceToAuth"></param>
        /// <returns></returns>
        private static Device PrepareDeviceForAuth(Device deviceToAuth)
        {
            return new Device()
            {
                Name = deviceToAuth.Name,
                PhoneNumber = deviceToAuth.PhoneNumber,
                IdCiscoAsa = deviceToAuth.IdCiscoAsa,
                ScreenSize = deviceToAuth.ScreenSize,
                ScreenResolution = deviceToAuth.ScreenResolution,
                Camera = deviceToAuth.Camera,
                SdMemoryCard = deviceToAuth.SdMemoryCard,
                GmailAccount = deviceToAuth.GmailAccount,
                Comments = deviceToAuth.Comments,
                Status = DeviceStatus.Authorized
            };
        }
    }
}
