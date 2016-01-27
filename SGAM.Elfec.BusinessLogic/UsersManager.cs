using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Security;
using System.Collections.Generic;

namespace SGAM.Elfec.BusinessLogic
{
    public class UsersManager
    {
        /// <summary>
        /// Registra un usuario en la api para su uso y asignación en la aplicación
        /// </summary>
        /// <param name="userToRegister">usuario a registrar</param>
        /// <param name="callback">callback</param>
        public static void RegisterUser(User userToRegister, ResultCallback<User> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var restInvoker = new RestInvoker<User>();
            restInvoker.InvokeWebService(callback, () =>
            {
                return RestEndpointFactory
                    .Create<IUsersEndpoint>(user.Username, user.AuthenticationToken)
                    .RegisterUser(userToRegister);
            });
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en el sistema, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback">callback para el proceso asincrono</param>
        /// <param name="userStatus">Opcional, estado para filtrar usuarios, si no se especifica se obtienen todos
        /// los usuarios registrados en la aplicación</param>
        public static void GetAllUsers(ResultCallback<IList<User>> callback, UserStatus? userStatus = null)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var restInvoker = new RestInvoker<IList<User>>();
            restInvoker.InvokeWebService(callback, () =>
            {
                var parameters = new Dictionary<string, string>();
                parameters["sort"] = "-status,username";
                if (userStatus != null)
                    parameters["status"] = ((int)userStatus).ToString();
                return RestEndpointFactory
                    .Create<IUsersEndpoint>(user.Username, user.AuthenticationToken)
                    .GetAllUsers(parameters);
            });
        }
    }
}
