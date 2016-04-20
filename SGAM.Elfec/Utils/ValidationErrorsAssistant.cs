using SGAM.Elfec.Helpers.Utils;
using System.Collections;
using System.Collections.Generic;
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
        public static void UpdateSources(FrameworkElement root)
        {
            IList items = new List<object>();
            if (root is Panel)
                items.AddRange((root as Panel).Children);
            if (root is ItemsControl)
                items.AddRange((root as ItemsControl).Items);
            if (root is ContentControl && (root as ContentControl).Content is FrameworkElement)
                items.Add((root as ContentControl).Content as FrameworkElement);
            UpdateItemSource(root);
            foreach (object item in items)
            {
                if (item is FrameworkElement)
                    UpdateSources(item as FrameworkElement);
            }
        }

        private static void UpdateItemSource(FrameworkElement item)
        {
            if (item is TextBox)
                (item as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            else if (item is PasswordBox)
                (item as PasswordBox).GetBindingExpression(PasswordBoxAssistant.BoundPassword).UpdateSource();
        }

        /// <summary>
        /// Limpia todos los errores en las validaciones de los campos
        /// </summary>
        /// <param name="root"></param>
        public static void ClearErrors(FrameworkElement root)
        {
            IList items = new List<object>();
            if (root is Panel)
                items.AddRange((root as Panel).Children);
            if (root is ItemsControl)
                items.AddRange((root as ItemsControl).Items);
            if (root is ContentControl && (root as ContentControl).Content is FrameworkElement)
                items.Add((root as ContentControl).Content as FrameworkElement);
            ClearItemErrors(root);
            foreach (object item in items)
            {
                if (item is FrameworkElement)
                    ClearErrors(item as FrameworkElement);
            }
        }

        private static void ClearItemErrors(FrameworkElement item)
        {
            if (item is TextBox)
                Validation.ClearInvalid((item as TextBox).GetBindingExpression(TextBox.TextProperty));
            else if (item is PasswordBox)
                Validation.ClearInvalid((item as PasswordBox).GetBindingExpression(PasswordBoxAssistant.BoundPassword));
        }
    }
}
