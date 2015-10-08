using FluentValidation.Results;
using SGAM.Elfec.Model.Validations;
using SGAM.Elfec.Model.Validations.Validators;
using System;

namespace SGAM.Elfec.Model.WebServices
{
    public class RemoteSession : BaseEntity
    {
        public String Username { get; set; }
        public String Password { get; set; }

        public RemoteSession() { }

        public RemoteSession(String username, String password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<RemoteSessionValidator, RemoteSession>(this);
        }
    }
}
