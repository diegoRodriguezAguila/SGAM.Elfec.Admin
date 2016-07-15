using SGAM.Elfec.Model;
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
        /// <param name="error">error</param>
        void ShowAPKLoadError(Exception error);
        /// <summary>
        /// Muestra al usuario que se finalizó la carga del apk
        /// </summary>
        void OnAPKLoadFinished();

        /// <summary>
        /// Indica al usuario que debe esperar, se esta subiendo la aplicación
        /// </summary>
        void ShowUploadingAplication();
        /// <summary>
        /// Muestra los errores de subida de la aplicación
        /// </summary>
        /// <param name="error">error</param>
        void ShowAplicationUploadError(Exception error);
        /// <summary>
        /// Muestra al usuario que se finalizó la subida de la aplicación exitosamente
        /// </summary>
        /// <param name="application"></param>
        void ShowAplicationUploadedSuccessfully(Application application);
    }
}
