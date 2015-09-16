using Newtonsoft.Json;
using RestEase;
using SGAM.Elfec.WebServices.ApiEndpoints;
using SGAM.Elfec.WebServices.JsonContractResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.WebServices
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
                ContractResolver = new SnakeCasePropertyNamesContractResolver()
            };
            return RestClient.For<T>(BASE_URL, settings);
        }
    }
}
