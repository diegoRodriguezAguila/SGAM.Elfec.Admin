using FluentValidation;
using SGAM.Elfec.Model.WebServices;

namespace SGAM.Elfec.Model.Validations.Validators
{
    public class RemoteSessionValidator : AbstractValidator<RemoteSession>
    {
        public RemoteSessionValidator()
        {
            RuleFor(remoteSession => remoteSession.Username).NotEmpty().WithName("usuario")
                .WithMessage("El {PropertyName} no puede estar vacío");
            RuleFor(remoteSession => remoteSession.Password).NotEmpty().WithName("constraseña")
                .WithMessage("Escriba su {PropertyName}");

        }
    }
}
