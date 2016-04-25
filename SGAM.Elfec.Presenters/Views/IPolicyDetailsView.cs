using SGAM.Elfec.Model;
using System;

namespace SGAM.Elfec.Presenters.Views
{
    /// <summary>
    /// Vista de detalles de una directiva de usuario
    /// </summary>
    public interface IPolicyDetailsView : IBaseView
    {
        void AddRule(Policy policy);
        /// <summary>
        /// Pide al usuario confirmación de eliminación
        /// </summary>
        /// <returns></returns>
        bool DeleteConfirmation();
        /// <summary>
        /// Indica al usuario que se esta eliminando la regla
        /// </summary>
        void DeletingRule();
        /// <summary>
        /// Indica al usuario que se eliminaron las reglas exitosamente!
        /// </summary>
        void RuleDeleted();

        /// <summary>
        /// Infroma al usuario que hubo un error al eliminar la regla
        /// </summary>
        /// <param name="error">error</param>
        void ErrorDeleting(Exception error);
    }
}
