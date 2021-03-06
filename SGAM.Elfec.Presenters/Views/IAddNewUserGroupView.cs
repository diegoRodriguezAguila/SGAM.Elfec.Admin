﻿using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IAddNewUserGroupView : IBaseView
    {
        /// <summary>
        /// Indica al usuario que debe esperar, se esta registrando un grupo de usuarios
        /// </summary>
        void ShowRegisteringUserGroup();
        /// <summary>
        /// Muestra cualquier error de registro de un grupo de usuarios
        /// </summary>
        /// <param name="error">errores</param>
        void ShowRegistrationError(Exception error);
        /// <summary>
        /// Muestra al usuario que se finalizó el registro del grupo de usuarios exitosamente
        /// </summary>
        /// <param name="userGroup"></param>
        void ShowUserGroupRegistered(UserGroup userGroup);
    }
}
