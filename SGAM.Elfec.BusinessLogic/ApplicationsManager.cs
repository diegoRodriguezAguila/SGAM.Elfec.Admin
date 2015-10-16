using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Security;
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
            var restInvoker = new RestInvoker<IList<Application>>();
            restInvoker.InvokeWebService(callback, () =>
            {
                var parameters = new Dictionary<string, string>();
                parameters["sort"] = "status:desc,name";
                return RestEndpointFactory
                    .Create<IApplicationsEndpoint>(user.Username, user.AuthenticationToken).GetAllApplications(parameters);
            });
        }
    }
}
