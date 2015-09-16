using RestEase;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.WebServices;
using System.Threading.Tasks;

namespace SGAM.Elfec.WebServices.ApiEndpoints
{
    public interface ISessionsEndpoint : ISgamApiEndpoint
    {
        // The "sessions" is a relative path the a base URL, which we'll provide later
        [Post("/sessions")]
        Task<User> LogIn([Body] RemoteSession session);
    }
}
