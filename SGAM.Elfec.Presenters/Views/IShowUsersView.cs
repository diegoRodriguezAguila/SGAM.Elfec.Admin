using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IShowUsersView : ILoadingContentErrorView
    {
        void ProcessingStatusChange();
        void ErrorChangingStatus(Exception error);
        void StatusChanged();
        void ShowUserDetails(User user);
    }
}
