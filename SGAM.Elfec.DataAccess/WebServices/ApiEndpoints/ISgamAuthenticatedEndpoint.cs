using RestEase;
using System;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    /// <summary>
    /// Representa un endpoint con autenticación
    /// </summary>
    [Obsolete("Should not be used until RestEase gets patched")]
    public interface ISgamAuthenticatedEndpoint : ISgamApiEndpoint
    {
        [Header("X-Api-Token")]
        string ApiToken { get; set; }
        [Header("X-Api-Username")]
        string ApiUsername { get; set; }
    }
}
