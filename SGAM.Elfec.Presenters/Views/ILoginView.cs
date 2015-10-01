using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    /// <summary>
    /// Abstracción de la vista de login
    /// </summary>
    public interface ILoginView : IBaseView, IWaitingView
    {
        string Username { get; }
        string Password { get; }
        /// <summary>
        /// Muestra los errores ocurridos durante el intento de login
        /// </summary>
        /// <param name="validationErrors">errores de validación de lógin</param>
        void ShowLoginErrors(params Exception[] validationErrors);
        /// <summary>
        /// Notifica que el usuario se logeó exitosamente
        /// </summary>
        /// <param name="loggedUser"></param>
        void NotifySuccessfulLogin(User loggedUser);
    }
}
