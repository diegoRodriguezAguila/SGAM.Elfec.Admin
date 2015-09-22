using Newtonsoft.Json;
using RestEase;
using SGAM.Elfec.Model.Exceptions;
using SGAM.Elfec.Model.WebServices;
using System;
using System.Net;

namespace SGAM.Elfec.DataAccess.WebServices
{
    /// <summary>
    /// Se encarga de interpretar el error de la api adecuadamente
    /// </summary>
    public class RestErrorInterpreter
    {
        /// <summary>
        /// interpreta el error correcto según su código y tipo de excepción
        /// </summary>
        /// <param name="apiException"></param>
        /// <returns>el error interpretado</returns>
        public static Exception InterpretError(ApiException apiException)
        {
            if (apiException.StatusCode == HttpStatusCode.InternalServerError)
                return new ServerSideException();
            RestErrorResponse error = JsonConvert.DeserializeObject<RestErrorResponse>(apiException.Content);
            return new Exception(error.Errors);
        }
    }
}
