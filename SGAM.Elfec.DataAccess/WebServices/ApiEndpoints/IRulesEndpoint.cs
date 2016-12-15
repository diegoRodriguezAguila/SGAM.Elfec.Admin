using RestEase;
using SGAM.Elfec.Model;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IRulesEndpoint : ISgamAuthenticatedEndpoint
    {
        [Put("/rules/{ruleId}")]
        Task<Rule> Update([Path] string ruleId, [Body] Rule ruleUpdate);
        [Post("/rules/{ruleId}/entities/{entityIds}")]
        Task<Rule> AddEntities([Path] string ruleId, [Path] string entityIds);
        [Delete("/rules/{ruleId}/entities/{entityIds}")]
        Task<Rule> DeleteEntities([Path] string ruleId, [Path] string entityIds);
    }
}
