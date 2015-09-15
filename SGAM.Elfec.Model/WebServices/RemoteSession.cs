using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Model.WebServices
{
    public class RemoteSession
    {
        public String Username { get; set; }
        public String Password { get; set; }

        public RemoteSession() { }

        public RemoteSession(String username, String password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
