using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IAddNewUserView : ILoadingContentErrorView
    {
        /// <summary>
        /// Indica al usuario que debe esperar, se esta registrando un usuario
        /// </summary>
        void ShowRegisteringUser();
        /// <summary>
        /// Muestra los errores de registro de usuario
        /// </summary>
        /// <param name="errors">errores</param>
        void ShowRegistrationErrors(params Exception[] errors);
        /// <summary>
        /// Muestra al usuario que se finalizó el registro de usuario exitosamente
        /// </summary>
        /// <param name="user"></param>
        void ShowUserRegisteredSuccessfully(User user);
    }
}
