namespace SGAM.Elfec.Model
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
    /// <summary>
    /// Modelo de Dispositivos
    /// </summary>
    public class Device
    {
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Serial { get; set; }
        public string WifiMacAddress { get; set; }
        public string BluetoothMacAddress { get; set; }
        public string Platform { get; set; }
        public string OsVersion { get; set; }
        public string BasebandVersion { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PhoneNumber { get; set; }
        public string IdCiscoAsa { get; set; }
        public double? ScreenSize { get; set; }
        public string ScreenResolution { get; set; }
        public double? Camera { get; set; }
        public double? SdMemoryCard { get; set; }
        public string GmailAccount { get; set; }
        public string Comments { get; set; }
        private short _status;
        public DeviceStatus Status { get { return (DeviceStatus)_status; } set { _status = (short)value; } }
    }
}
