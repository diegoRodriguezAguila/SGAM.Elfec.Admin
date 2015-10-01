using Newtonsoft.Json;
using RestEase;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.DataAccess.WebServices.JsonContractResolver;
using System;

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
        public static T Create<T>()
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new SnakeCasePropertyNamesContractResolver()
            };
            return RestClient.For<T>(BASE_URL, settings);
        }

        /// <summary>
        /// Crea un endpoint Rest  con la url por defecto <see cref="BASE_URL"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">usuario para autenticación</param>
        /// <param name="authToken">token para autenticación</param>
        /// <returns>Punto de acceso al webservice rest</returns>
        public static T Create<T>(string username, string authToken) where T : ISgamApiEndpoint
        {
            T endpoint = Create<T>();
            if (username != null && authToken != null)
            {
                (endpoint as IDevicesEndpoint).ApiToken = authToken;
                (endpoint as IDevicesEndpoint).ApiUsername = username;
            }
            return endpoint;
        }
    }
}
