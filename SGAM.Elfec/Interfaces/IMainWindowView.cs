﻿using System.Windows.Controls;

namespace SGAM.Elfec.Interfaces
{
    public interface IMainWindowView
    {
        void CloseWindow();
        void ChangeTitle(string title);
        void ChangeToApplicationsView(bool force = false);
        void ChangeToDevicesView(bool force = false);
        void ChangeToAllDeviceGroupsView(bool force = false);
        void ChangeToView<T>(T view) where T : Control;
        void GoBack();
        void SetStatusBar(string status);
        void SetStatusBarDefault();
        /// <summary>
        /// Muestra un mensaje con su titulo al usuario
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void NotifyUser(string title, string message);
    }
}
