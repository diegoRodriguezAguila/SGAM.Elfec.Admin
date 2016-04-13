using Newtonsoft.Json;
using RestEase;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.DataAccess.WebServices.Converters;
using SGAM.Elfec.DataAccess.WebServices.JsonContractResolver;

namespace SGAM.Elfec.DataAccess.WebServices
{
    /// <summary>
    /// Factory para obtener el Endpoint de los webservices
    /// Utilizando la configuración de RestAdapter necesaria
    /// </summary>
    public class RestEndpointFactory
    {
        private static JsonSerializerSettings _settings;
        static RestEndpointFactory()
        {
            _settings = new JsonSerializerSettings()
            {
                ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _settings.Converters.Add(new SnakeCaseEnumConverter { AllowIntegerValues = true });
            _settings.Converters.Add(new EntityConverter());
        }
        /// <summary>
        /// Crea un endpoint Rest  con la url por defecto <see cref="Settings.Properties.SGAM.Default.BaseApiURL"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Punto de acceso al webservice rest</returns>
        public static T Create<T>() where T : ISgamApiEndpoint
        {
            return RestClient.For<T>(Settings.Properties.SGAM.Default.BaseApiURL, _settings);
        }
        /// <summary>
        /// Crea un endpoint Rest  con la url por defecto <see cref="BASE_URL"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">usuario para autenticación</param>
        /// <param name="authToken">token para autenticación</param>
        /// <returns>Punto de acceso al webservice rest</returns>
        public static T Create<T>(string username, string authToken) where T : ISgamAuthenticatedEndpoint
        {
            T endpoint = Create<T>();
            if (username != null && authToken != null)
            {
                endpoint.ApiToken = authToken;
                endpoint.ApiUsername = username;
            }
            return endpoint;
        }
    }
}
