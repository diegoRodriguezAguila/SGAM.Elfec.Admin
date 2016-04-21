using SGAM.Elfec.Model;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IAddRuleView : IProcessSuccessErrorView<Rule>
    {
        /// <summary>
        /// Llama a las validaciones necesarias en todos los campos de la vista que
        /// necesiten ser validados
        /// </summary>
        void Validate();
        /// <summary>
        /// Notifica al usuario que los campos tienen errores y que debe corregir para proceder
        /// </summary>
        void NotifyErrorsInFields();
    }
}
