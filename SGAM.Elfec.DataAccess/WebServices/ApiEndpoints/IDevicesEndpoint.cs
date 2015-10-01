using RestEase;
using SGAM.Elfec.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IDevicesEndpoint : ISgamApiEndpoint
    {
        [Header("X-Api-Token")]
        string ApiToken { get; set; }
        [Header("X-Api-Username")]
        string ApiUsername { get; set; }
        // The "devices" is a relative path the a base URL, which we'll provide later
        [Get("/devices")]
        Task<IList<Device>> GetAllDevices([QueryMap] IDictionary<string, object> filters);
    }
}
