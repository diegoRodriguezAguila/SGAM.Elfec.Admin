namespace SGAM.Elfec.Model.Enums
{
    /// <summary>
    /// Enum de los estados de un dispositivo
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// Estado de dispositivo inhabilitado para el uso del sistema
        /// </summary>
        Unauthorized,
        /// <summary>
        /// Estado de dispositivo habilitado para el uso del sistema
        /// </summary>
        Authorized,
        /// <summary>
        /// Estado de dispositvo pendiente de autorización
        /// </summary>
        AuthPending
    }
}
