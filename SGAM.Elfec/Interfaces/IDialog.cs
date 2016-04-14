namespace SGAM.Elfec.Interfaces
{
    public interface IDialog
    {
        bool? DialogResult { get; }
        /// <summary>
        /// Shows the control in a dialog window
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();
        /// <summary>
        /// Closes the dialog
        /// </summary>
        void Close();
    }
}
