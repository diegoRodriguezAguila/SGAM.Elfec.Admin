using RestEase;
using SGAM.Elfec.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IUsersEndpoint : ISgamAuthenticatedEndpoint
    {
        // The "users" is a relative path the a base URL, which we'll provide later
        [Post("/users")]
        Task<User> RegisterUser([Body] User userToRegister);

        // The "users" is a relative path the a base URL, which we'll provide later
        [Get("/users")]
        Task<IList<User>> GetAllUsers([QueryMap] IDictionary<string, string> filters);
    }
}
