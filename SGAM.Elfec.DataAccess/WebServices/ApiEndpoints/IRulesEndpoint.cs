using RestEase;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IRulesEndpoint : ISgamAuthenticatedEndpoint
    {
        // The "rules" is a relative path the a base URL, which we'll provide later
        [Post("/rules/{ruleId}/entities/{entityIds}")]
        Task AddEntities([Path] string ruleId, [Path] string entityIds);
    }
}
