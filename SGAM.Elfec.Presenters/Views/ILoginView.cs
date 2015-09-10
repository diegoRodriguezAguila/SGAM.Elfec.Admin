using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        void ShowLoginErrors(IList<Exception> validationErrors);
    }
}
