using System;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    public class User
    {
        public String Username { get; set; }
        public String AuthenticationToken { get; set; }
        public IList<Role> Roles { get; set; }

        public User() { }

        public User(String username, String authenticationToken)
        {
            this.Username = username;
            this.AuthenticationToken = authenticationToken;
        }
    }
}
