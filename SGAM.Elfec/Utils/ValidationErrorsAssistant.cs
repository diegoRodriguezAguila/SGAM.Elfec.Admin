using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec.Utils
{
    /// <summary>
    /// Clase de asistencia para la validación de errores
    /// </summary>
    public static class ValidationErrorsAssistant
    {
        /// <summary>
        /// Obliga a todos los campos hijos a realizar sus respectivas validaciones
        /// </summary>
        /// <param name="root"></param>
        public static void UpdateSources(Panel root)
        {
            foreach (FrameworkElement item in root.Children)
            {
                if (item is TextBox)
                    (item as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
                else if (item is PasswordBox)
                    (item as PasswordBox).GetBindingExpression(PasswordBoxAssistant.BoundPassword).UpdateSource();
            }
        }

        /// <summary>
        /// Limpia todos los errores en las validaciones de los campos
        /// </summary>
        /// <param name="root"></param>
        public static void ClearErrors(Panel root)
        {
            foreach (FrameworkElement item in root.Children)
            {
                if (item is TextBox)
                    Validation.ClearInvalid((item as TextBox).GetBindingExpression(TextBox.TextProperty));
                else if (item is PasswordBox)
                    Validation.ClearInvalid((item as PasswordBox).GetBindingExpression(PasswordBoxAssistant.BoundPassword));
            }
        }
    }
}
