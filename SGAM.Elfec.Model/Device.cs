using FluentValidation.Results;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Model.Validations;
using SGAM.Elfec.Model.Validations.Validators;

namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Modelo de Dispositivos
    /// </summary>
    public class Device : BaseEntity
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
        public DeviceStatus Status { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<DeviceValidator, Device>(this);
        }
    }
}
