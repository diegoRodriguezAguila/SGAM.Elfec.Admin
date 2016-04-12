using SGAM.Elfec.Model;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IPolicyRulesView : ILoadingContentErrorView
    {
        void PolicyDetails(Policy policy);
    }
}
