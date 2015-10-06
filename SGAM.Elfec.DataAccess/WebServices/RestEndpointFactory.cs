using Newtonsoft.Json;
using RestEase;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.DataAccess.WebServices.JsonContractResolver;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices
{
    /// <summary>
    /// Factory para obtener el Endpoint de los webservices
    /// Utilizando la configuración de RestAdapter necesaria
    /// </summary>
    public class RestEndpointFactory
    {
        /// <summary>
        /// La URL de los web services de SGAM, si fuera necesario conectar a otro webservice
        ///  se puede pasar otra URL
        /// </summary>
        public const String BASE_URL = "http://192.168.50.56:3000/api/";

        /// <summary>
        /// Crea un endpoint Rest  con la url por defecto <see cref="BASE_URL"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Punto de acceso al webservice rest</returns>
        public static T Create<T>() where T : ISgamApiEndpoint
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            return RestClient.For<T>(BASE_URL, PutHeaders, settings);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Se encarga de modificar el request poniendo los headers adecuados
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task PutHeaders(HttpRequestMessage request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (request.Content != null)
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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
