using System.Windows.Controls;

namespace SGAM.Elfec.Interfaces
{
    public interface IMainWindowService
    {
        void CloseWindow();
        /// <summary>
        /// Asigna el titulo de la ventana
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IMainWindowService WindowTitle(string title);
        /// <summary>
        /// Cambia la vista actual a la de aplicaciones y limpia la pila de vistas
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        IMainWindowService ApplicationsView(bool force = false);
        /// <summary>
        /// Cambia la vista actual a la de dispositivos y limpia la pila de vistas
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        IMainWindowService DevicesView(bool force = false);
        /// <summary>
        /// Cambia la vista actual a la de usuarios y limpia la pila de vistas
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        IMainWindowService UsersView(bool force = false);
        /// <summary>
        /// Asigna la vista actual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        IMainWindowService CurrentView<T>(T view) where T : Control;
        /// <summary>
        /// Va a la anterior vista
        /// </summary>
        /// <returns></returns>
        IMainWindowService Back();
        /// <summary>
        /// Asigna el mensaje de estado a la status bar
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IMainWindowService StatusBar(string status);
        /// <summary>
        /// Pone la status bar por defecto
        /// </summary>
        /// <returns></returns>
        IMainWindowService StatusBarDefault();
        /// <summary>
        /// Muestra un mensaje con su titulo al usuario
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        IMainWindowService NotifyUser(string title, string message);
    }
}
