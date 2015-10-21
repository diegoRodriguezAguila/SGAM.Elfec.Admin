using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IAddNewApplicationView : IBaseView
    {
        /// <summary>
        /// Indica al usuario que debe esperar
        /// </summary>
        void ShowLoadingAPK();
        /// <summary>
        /// Muestra los errores de carga del apk
        /// </summary>
        /// <param name="errors">errores</param>
        void ShowAPKLoadErrors(params Exception[] errors);
        /// <summary>
        /// Muestra al usuario que se finalizó la carga del apk
        /// </summary>
        void OnAPKLoadFinished();
    }
}
