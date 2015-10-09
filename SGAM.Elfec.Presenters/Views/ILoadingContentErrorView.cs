using System;

namespace SGAM.Elfec.Presenters.Views
{
    /// <summary>
    /// Interfáz que representa las vistas que tienen 3 partes la de carga de datos
    /// muestra de datos o muestra de errores
    /// </summary>
    public interface ILoadingContentErrorView : IBaseView
    {
        /// <summary>
        /// Se ejecuta cuando se inicia la carga de datos, o refresh
        /// </summary>
        /// <param name="isRefresh">true si es que es refresh</param>
        void OnLoadingData(bool isRefresh = false);

        /// <summary>
        /// Se ejecuta cuando hubo errores en la carga de datos o refresh
        /// </summary>
        /// <param name="isRefresh">si era carga nueva o refresh</param>
        /// <param name="errors">errores</param>
        void OnLoadingErrors(bool isRefresh = false, params Exception[] errors);

        /// <summary>
        /// Se ejecuta cuando finalizó la carga de datos exitosamente
        /// </summary>
        void OnDataLoaded();

    }
}
