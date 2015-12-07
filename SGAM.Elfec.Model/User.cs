using SGAM.Elfec.Model.Enums;
using System;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    public class User
    {
        public string Username { get; set; }
        public string AuthenticationToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string CompanyArea { get; set; }
        public Uri PhotoUrl { get; set; }

        public IList<Role> Roles { get; set; }

        public UserStatus Status { get; set; }

        public User() { }

        public User(String username, String authenticationToken)
        {
            this.Username = username;
            this.AuthenticationToken = authenticationToken;
        }
    }
}
