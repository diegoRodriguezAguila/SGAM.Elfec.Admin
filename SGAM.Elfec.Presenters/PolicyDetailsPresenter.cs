using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;

namespace SGAM.Elfec.Presenters
{
    public class PolicyDetailsPresenter : BasePresenter<IPolicyDetailsView>
    {
        public PolicyDetailsPresenter(IPolicyDetailsView view, Policy policy) : base(view)
        {

        }

    }
}
