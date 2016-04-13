using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Security;
using System.Collections.Generic;

namespace SGAM.Elfec.BusinessLogic
{
    public class PolicyManager
    {
        /// <summary>
        /// Obtiene todos los dipositivos, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAllPolicies(ResultCallback<IList<Policy>> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,type";
            parameters["include"] = "rules.entities";
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IPoliciesEndpoint>(user.Username, user.AuthenticationToken)
                    .GetAllPolicies(parameters));
        }
    }
}
