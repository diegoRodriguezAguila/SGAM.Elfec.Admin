using RestEase;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    /// <summary>
    /// Representa un endpoint con autenticación
    /// </summary>
    public interface ISgamAuthenticatedEndpoint : ISgamApiEndpoint
    {
        [Header("X-Api-Token")]
        string ApiToken { get; set; }
        [Header("X-Api-Username")]
        string ApiUsername { get; set; }
    }
}
