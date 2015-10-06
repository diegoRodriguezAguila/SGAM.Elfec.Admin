using RestEase;
using SGAM.Elfec.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IDevicesEndpoint : ISgamAuthenticatedEndpoint
    {
        // The "devices" is a relative path the a base URL, which we'll provide later
        [Get("/devices")]
        Task<IList<Device>> GetAllDevices([QueryMap] IDictionary<string, string> filters);
        [Put("/devices/{imei}")]
        Task<Device> UpdateDevice([Path] string imei, [Body] Device deviceToUpdate);

    }
}
