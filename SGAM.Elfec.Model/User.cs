using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Model
{
    public class User
    {
        public String Username { get; set; }
        public String AuthenticationToken { get; set; }

        public User() { }

        public User(String username, String authenticationToken)
        {
            this.Username = username;
            this.AuthenticationToken = authenticationToken;
        }
    }
}
