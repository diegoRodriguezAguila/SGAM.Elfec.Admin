using RestEase;
using SGAM.Elfec.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IApplicationsEndpoint : ISgamAuthenticatedEndpoint
    {
        // The "applications" is a relative path the a base URL, which we'll provide later
        [Get("/applications")]
        Task<IList<Application>> GetAllApplications([QueryMap] IDictionary<string, string> filters);
        [Put("/applications/{package}/{version}/status")]
        Task<Application> UpdateAppVersionStatus([Path] string package, [Path] string version,
            [Body] AppVersion appStatus);
    }
}
