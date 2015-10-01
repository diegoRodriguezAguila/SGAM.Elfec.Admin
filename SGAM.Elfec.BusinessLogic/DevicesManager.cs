using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
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
            var restInvoker = new RestInvoker<IList<Device>>();
            restInvoker.InvokeWebService(callback, () =>
            {
                return RestEndpointFactory
                    .Create<IDevicesEndpoint>("drodriguezd", "1KFcx4VxRs5aqYum9cro").GetAllDevices(null);
            });
        }
    }
}
