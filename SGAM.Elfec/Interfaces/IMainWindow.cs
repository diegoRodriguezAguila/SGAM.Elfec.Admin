using System.Windows.Controls;

namespace SGAM.Elfec.Interfaces
{
    public interface IMainWindow
    {
        void CloseWindow();
        /// <summary>
        /// Cambia la vista actual a la de aplicaciones y limpia la pila de vistas
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        IMainWindow ApplicationsView(bool force = false);
        /// <summary>
        /// Cambia la vista actual a la de dispositivos y limpia la pila de vistas
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        IMainWindow DevicesView(bool force = false);
        /// <summary>
        /// Cambia la vista actual a la de usuarios y limpia la pila de vistas
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        IMainWindow UsersView(bool force = false);
        /// <summary>
        /// Asigna la vista actual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        IMainWindow CurrentView<T>(T view) where T : Control;
        /// <summary>
        /// Va a la anterior vista
        /// </summary>
        /// <returns></returns>
        IMainWindow Back();
        /// <summary>
        /// Va a la vista siguiente, si es que la hay
        /// </summary>
        /// <returns></returns>
        IMainWindow Forward();
        /// <summary>
        /// Asigna el mensaje de estado a la status bar
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IMainWindow StatusBar(string status);
        /// <summary>
        /// Pone la status bar por defecto
        /// </summary>
        /// <returns></returns>
        IMainWindow StatusBarDefault();
        /// <summary>
        /// Muestra un mensaje con su titulo al usuario
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        IMainWindow NotifyUser(string title, string message);
    }
}
