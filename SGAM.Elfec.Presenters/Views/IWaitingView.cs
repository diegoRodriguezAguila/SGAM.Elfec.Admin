using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGAM.Elfec.Presenters.Views
{

    /// <summary>
    /// Abstracción de una vista en la que se debe realizar una espera
    /// </summary>
    public interface IWaitingView : IBaseView
    {
        /// <summary>
        /// Indica al usuario que debe esperar
        /// </summary>
        void ShowWaiting();

        /// <summary>
         /// Acualiza el mensaje de espera del usuario
         /// </summary>
        void UpdateWaiting(string waitingMessage);

        /// <summary>
         /// Borra el mensaje de espera
         /// </summary>
        void HideWaiting();
    }
}
