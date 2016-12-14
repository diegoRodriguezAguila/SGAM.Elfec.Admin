using RestEase;
using SGAM.Elfec.Model;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IRulesEndpoint : ISgamAuthenticatedEndpoint
    {
        [Put("/rules/{ruleId}")]
        Task<Rule> Update([Path] string ruleId, [Body] Rule ruleUpdate);
        // The "rules" is a relative path the a base URL, which we'll provide later
        [Post("/rules/{ruleId}/entities/{entityIds}")]
        Task AddEntities([Path] string ruleId, [Path] string entityIds);
    }
}
