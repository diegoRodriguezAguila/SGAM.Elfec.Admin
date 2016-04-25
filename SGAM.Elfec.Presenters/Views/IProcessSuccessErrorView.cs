using System;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IProcessSuccessErrorView : IBaseView
    {
        /// <summary>
        /// Se ejecuta cuando se inicia el procesamiento de datos
        /// </summary>
        void ProcessingData();
        /// <summary>
        /// Se ejecuta cuando finalizó el procesamiento de datos exitosamente
        /// </summary>
        void Success();

        /// <summary>
        /// Se ejecuta cuando hubo errores en el procesamiento de datos
        /// </summary>
        /// <param name="error">error</param>
        void Error(Exception error);
    }
    /// <summary>
    /// Interfáz que representa las vistas que tienen 3 partes la de procesamiento de datos,
    /// el éxito del proceso o el error
    /// </summary>
    public interface IProcessSuccessErrorView<T> : IBaseView
    {
        /// <summary>
        /// Se ejecuta cuando se inicia el procesamiento de datos
        /// </summary>
        void ProcessingData();
        /// <summary>
        /// Se ejecuta cuando finalizó el procesamiento de datos exitosamente
        /// </summary>
        void Success(T data);

        /// <summary>
        /// Se ejecuta cuando hubo errores en el procesamiento de datos
        /// </summary>
        /// <param name="error">error</param>
        void Error(Exception error);

    }
}
