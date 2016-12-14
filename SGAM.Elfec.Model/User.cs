using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    public class User : IEntity
    {
        public string Username { get; set; }
        public string AuthenticationToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Position { get; set; }
        public string CompanyArea { get; set; }
        public Uri PhotoUrl { get; set; }

        public IList<Role> Roles { get; set; }

        public UserStatus Status { get; set; }

        public User() { }

        public User(string username, string authenticationToken)
        {
            this.Username = username;
            this.AuthenticationToken = authenticationToken;
        }

        /// <summary>
        /// Verifica si el usuario actual se puede autenticar
        /// para ello tiene que tener asignados los valores del token de autenticación
        /// y el nombre de usuario
        /// </summary>
        public bool IsAuthenticable { get { return Username != null && AuthenticationToken != null; } }

        #region Entity Methods
        public string EntityType => GetType().Name;
        public string Id => Username;
        public string Name => Username;
        public string Details => FullName;

        #endregion
    }
}
