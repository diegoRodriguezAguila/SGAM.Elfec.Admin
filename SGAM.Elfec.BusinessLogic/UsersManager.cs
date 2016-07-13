using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace SGAM.Elfec.BusinessLogic
{
    public class UsersManager
    {
        /// <summary>
        /// Registra un usuario en la api para su uso y asignación en la aplicación
        /// </summary>
        /// <param name="userToRegister">usuario a registrar</param>
        public static IObservable<User> RegisterUser(User userToRegister)
        {
            return RestEndpointFactory
                     .Create<IUsersEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                     .RegisterUser(userToRegister).ToObservable()
                     .SubscribeOn(TaskPoolScheduler.Default)
                     .InterpretingErrors();
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en el sistema, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="userStatus">Opcional, estado para filtrar usuarios, si no se especifica se obtienen todos
        /// los usuarios registrados en la aplicación</param>
        public static IObservable<IList<User>> GetAllUsers(UserStatus? userStatus = null)
        {
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,username";
            if (userStatus != null)
                parameters["status"] = ((int)userStatus).ToString();
            return RestEndpointFactory
                    .Create<IUsersEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .GetAllUsers(parameters).ToObservable()
                     .SubscribeOn(TaskPoolScheduler.Default)
                     .InterpretingErrors();
        }
    }
}
