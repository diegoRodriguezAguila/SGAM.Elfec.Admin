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
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IUsersEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .RegisterUser(userToRegister));
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en el sistema, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback">callback para el proceso asincrono</param>
        /// <param name="userStatus">Opcional, estado para filtrar usuarios, si no se especifica se obtienen todos
        /// los usuarios registrados en la aplicación</param>
        public static void GetAllUsers(ResultCallback<IList<User>> callback, UserStatus? userStatus = null)
        {
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,username";
            if (userStatus != null)
                parameters["status"] = ((int)userStatus).ToString();
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IUsersEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .GetAllUsers(parameters));
        }
    }
}
