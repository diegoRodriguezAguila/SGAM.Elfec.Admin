using FluentValidation;
using System;

namespace SGAM.Elfec.Model.Validations.Validators
{
    public class DeviceValidator : AbstractValidator<Device>
    {
        public DeviceValidator()
        {
            RuleFor(device => device.Name).NotEmpty().WithName("nombre del dispositivo")
                .WithMessage("El {PropertyName} es obligatorio");
            RuleFor(device => device.Name).Length(4, 50).WithName("nombre del dispositivo")
                .WithMessage("El {PropertyName} debe tener al menos 5 caractéres, actualmente tiene {TotalLength}");
            RuleFor(device => device.ScreenSize).NotEmpty().WithName("tamaño de la pantalla")
                .WithMessage("El {PropertyName} no puede estar vacío");
            RuleFor(device => device.ScreenResolution).NotEmpty().WithName("resolución de la pantalla")
                 .WithMessage("La {PropertyName} no puede estar vacía");
            RuleFor(device => device.ScreenResolution).Matches("^\\d+[x]\\d+$").WithName("resolución de la pantalla")
                .WithMessage("La {PropertyName} debe tener el formato: [ancho]x[alto]")
                .Unless((d) => { return String.IsNullOrEmpty(d.ScreenResolution); });
            RuleFor(device => device.Camera).NotEmpty().WithName("resolución de la cámara")
                 .WithMessage("La {PropertyName} no puede estar vacía");
            RuleFor(device => device.GmailAccount).NotEmpty().WithName("cuenta de gmail")
                 .WithMessage("La {PropertyName} no puede estar vacía");
            RuleFor(device => device.GmailAccount).EmailAddress().WithName("cuenta de gmail")
                .WithMessage("La {PropertyName} tiene que ser una dirección de email válida")
                .Unless((d) => { return String.IsNullOrEmpty(d.GmailAccount); });
        }
    }
}
