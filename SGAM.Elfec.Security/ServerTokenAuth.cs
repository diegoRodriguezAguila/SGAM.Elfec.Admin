using SGAM.Elfec.Model;
using SGAM.Elfec.Model.WebServices;
using SGAM.Elfec.WebServices;
using SGAM.Elfec.WebServices.ApiEndpoints;
using System.Threading.Tasks;

namespace SGAM.Elfec.Security
{
    /// <summary>
    /// Clase que hace request del token con credenciales del usuario
    /// </summary>
    public class ServerTokenAuth
    {

        /// <summary>
        /// Se conecta remotamente a los webservices para solicitar un usuario con su token
        /// </summary>
        /// <param name="username">usuario a iniciar sesión</param>
        /// <param name="password">contraseña</param>
        /// <returns></returns>
        public Task<User> LogInAsync(string username, string password)
        {
            return RestEndpointFactory
                    .Create<ISessionsEndpoint>()
                .LogIn(new RemoteSession(username, password));
        }
    }
}
