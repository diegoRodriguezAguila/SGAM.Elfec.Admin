using System.Windows.Controls;

namespace SGAM.Elfec.Interfaces
{
    public interface IMainWindowView
    {
        void CloseWindow();
        void ChangeTitle(string title);
        void ChangeToAllAppsView();
        void ChangeToDevicesView();
        void ChangeToAllDeviceGroupsView();
        void ChangeToView<T>(T view) where T : Control;
        void GoBack();
        void SetStatusBar(string status);
        void SetStatusBarDefault();
    }
}
