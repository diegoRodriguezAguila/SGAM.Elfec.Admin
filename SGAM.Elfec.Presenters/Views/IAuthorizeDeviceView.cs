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
        /// <param name="error">error</param>
        void ShowAuthorizationError(Exception error);
        /// <summary>
        /// Muestra al usuario que se autorizó exitosamente el dispositivo
        /// </summary>
        /// <param name="device"></param>
        void ShowDeviceAuthorizedSuccessfuly(Device device);
        /// <summary>
        /// Notifica al usuario que los campos tienen errores y que debe corregir para proceder
        /// </summary>
        void NotifyErrorsInFields();
    }
}
