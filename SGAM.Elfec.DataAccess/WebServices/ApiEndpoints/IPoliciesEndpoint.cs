using RestEase;
using SGAM.Elfec.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IPoliciesEndpoint : ISgamAuthenticatedEndpoint
    {
        // The "policies" is a relative path the a base URL, which we'll provide later
        [Get("/policies")]
        Task<IList<Policy>> GetAllPolicies([QueryMap] IDictionary<string, string> filters);

        [Post("/policies/{policyId}/rules")]
        Task<Rule> RegisterRule([Path] string policyId, [Body] Rule rule);
    }
}
