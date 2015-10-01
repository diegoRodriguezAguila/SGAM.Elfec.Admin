namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Modelo de Dispositivos
    /// </summary>
    public class Device
    {
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public string Platform { get; set; }
        public string OsVersion { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PhoneNumber { get; set; }
        public double ScreenSize { get; set; }
        public string ScreenResolution { get; set; }
        public double Camera { get; set; }
        public double SdMemoryCard { get; set; }
        public int Status { get; set; }
    }
}
