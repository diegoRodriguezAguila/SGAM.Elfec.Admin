using SGAM.Elfec.Model;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IShowApplicationsView : ILoadingContentErrorView
    {
        void ShowApplicationDetails(Application application);
    }
}
