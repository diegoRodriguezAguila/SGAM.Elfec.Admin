using FluentValidation;

namespace SGAM.Elfec.Model.Validations.Validators
{
    public class PolicyRuleValidator : AbstractValidator<Rule>
    {
        public PolicyRuleValidator()
        {
            RuleFor(rule => rule.Name).NotEmpty().WithName("nombre")
                .WithMessage("El {PropertyName} es obligatorio");
            RuleFor(rule => rule.Name).Length(4, 50).WithName("nombre")
                .WithMessage("El {PropertyName} debe tener al menos 5 caractéres, actualmente tiene {TotalLength}");
            RuleFor(rule => rule.Value).NotEmpty().WithName("condición")
                .WithMessage("La {PropertyName} no puede estar vacía");
        }
    }
}
