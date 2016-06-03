using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IApplicationDetailsView : ILoadingContentErrorView
    {
        void ProcessingStatusChange();
        void ErrorChangingStatus(Exception error);
        void StatusChanged();
    }
}
