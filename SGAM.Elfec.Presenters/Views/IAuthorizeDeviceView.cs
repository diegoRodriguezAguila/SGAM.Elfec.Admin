using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    /// <summary>
    /// Abstracción de la vista de autorizar dispositivos
    /// </summary>
    public interface IAuthorizeDeviceView : IBaseView
    {
        /// <summary>
        /// Indica al usuario que debe esperar
        /// </summary>
        void ShowProcesingAuthorization();
        /// <summary>
        /// Muestra los errores de autorización
        /// </summary>
        /// <param name="errors">errores</param>
        void ShowAuthorizationErrors(params Exception[] errors);
        /// <summary>
        /// Muestra al usuario que se autorizó exitosamente el dispositivo
        /// </summary>
        /// <param name="device"></param>
        void ShowDeviceAuthorizedSuccessfuly(Device device);
    }
}
