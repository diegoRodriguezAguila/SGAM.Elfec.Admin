using RestEase;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;

namespace SGAM.Elfec.DataAccess.WebServices
{
    /// <summary>
    /// Factory para obtener el Endpoint de los webservices
    /// Utilizando la configuración de RestAdapter necesaria
    /// </summary>
    public class RestEndpointFactory
    {
        /// <summary>
        /// Crea un endpoint Rest  con la url por defecto <see cref="Settings.Properties.SGAM.Default.BaseApiURL"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Punto de acceso al webservice rest</returns>
        public static T Create<T>() where T : ISgamApiEndpoint
        {
            return RestClient.For<T>(Settings.Properties.SGAM.Default.BaseApiURL, JsonUtils.Settings);
        }
        /// <summary>
        /// Crea un endpoint Rest  con la url por defecto <see cref="BASE_URL"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">usuario para autenticación</param>
        /// <param name="authToken">token para autenticación</param>
        /// <returns>Punto de acceso al webservice rest</returns>
        public static T Create<T>(User authUser) where T : ISgamAuthenticatedEndpoint
        {
            T endpoint = Create<T>();
            if (authUser != null && authUser.IsAuthenticable)
            {
                endpoint.ApiToken = authUser.AuthenticationToken;
                endpoint.ApiUsername = authUser.Username;
            }
            return endpoint;
        }
    }
}
