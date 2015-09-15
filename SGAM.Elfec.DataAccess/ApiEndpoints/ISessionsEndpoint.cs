using RestEase;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.WebServices.ApiEndpoints
{
    public interface ISessionsEndpoint : IJsonEndpoint
    {
        // The [Get] attribute marks this method as a GET request
        // The "sessions" is a relative path the a base URL, which we'll provide later
        [Get("/sessions")]
        Task<List<User>> logIn([Body] RemoteSession session);
    }
}
