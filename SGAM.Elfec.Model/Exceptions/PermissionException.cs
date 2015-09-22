using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Model.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando un usuario no tiene los debidos permisos para utilizar una parte del sistema
    /// </summary>
    public class PermissionException : Exception
    {
        private User _user;
        private Permission _permission;

        public PermissionException(User user, Permission permission)
        {
            _permission = permission;
        }

        public override string Message
        {
            get
            {
                return "El usuario " + _user.Username + " no tiene " + _permission.Description;
            }
        }
    }
}
