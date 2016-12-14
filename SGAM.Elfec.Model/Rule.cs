using FluentValidation.Results;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Model.Interfaces;
using SGAM.Elfec.Model.Validations;
using SGAM.Elfec.Model.Validations.Validators;
using System.Collections.Generic;
using System.Linq;

namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Describes a policy rule
    /// </summary>
    public class Rule : BaseEntity
    {
        public string Id { get; set; }
        public RuleAction Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Exception { get; set; }
        public IList<IEntity> Entities { get; set; }
        public ApiStatus Status { get; set; }

        public string EntitiesString
        {
            get
            {
                if (Entities == null)
                    return null;
                return string.Join(", ", Entities.Select(e =>
                    $"{e.Name}({e.Details})"));
            }
        }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<PolicyRuleValidator, Rule>(this);
        }
    }
}
