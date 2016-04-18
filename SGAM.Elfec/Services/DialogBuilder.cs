using SGAM.Elfec.Interfaces;
using System.Windows.Controls;

namespace SGAM.Elfec.Services
{
    /// <summary>
    /// Servicio para la muestra de dialogos
    /// </summary>
    public class DialogBuilder : IDialogBuilder
    {
        private Control _content;
        private DialogBuilder(Control control)
        {
            _content = control;
        }

        /// <summary>
        /// Creates an instance for a dialog builder with a content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static IDialogBuilder For<T>(T control) where T : Control
        {
            return new DialogBuilder(control);
        }

        /// <summary>
        /// Builds the dialog
        /// </summary>
        /// <returns></returns>
        public IDialog Build()
        {
            var dialog = new DialogWindow();
            if (_content != null)
            {
                _content.Measure(new System.Windows.Size(double.MaxValue, double.MaxValue));
                dialog.Width = _content.DesiredSize.Width;
                dialog.Height = _content.DesiredSize.Height;
            }
            dialog.DialogContent.Child = _content;
            return dialog;
        }
    }
}
