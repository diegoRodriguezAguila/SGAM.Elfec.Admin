using System;

namespace SGAM.Elfec.Presenters.Views
{
    /// <summary>
    /// Interfáz que representa las vistas que tienen 3 partes la de carga de datos
    /// muestra de datos o muestra de errores
    /// </summary>
    public interface ILoadingContentErrorView<TContent> : IBaseView
    {
        /// <summary>
        /// Muestra la vista de espera de carga de datos, o de refresh
        /// </summary>
        /// <param name="isRefresh">true si es que es refresh</param>
        void ShowLoading(bool isRefresh = false);

        /// <summary>
        /// Muestra los errores de la carga de datos o el refresh
        /// </summary>
        /// <param name="isRefresh">si era carga nueva o refresh</param>
        /// <param name="errors">errores</param>
        void ShowErrors(bool isRefresh = false, params Exception[] errors);

        /// <summary>
        /// La información que debe mostrarse al finalizar la carga de datos
        /// </summary>
        /// <param name="data">información que debe mostrarse</param>
        void ShowContentData(TContent data);

    }
}
