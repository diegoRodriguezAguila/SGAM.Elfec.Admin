using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Model
{
    public class Permission
    {
        public static readonly Permission ADMIN_ACCESS = new Permission { Name = "ACCESO_APP_ADMINISTRADOR",
            Description = "Permiso para acceder a la aplicación administradora de aplicaciones móviles",
            Status = 1 };

        public String Name { get; set; }
        public String Description { get; set; }
        public short Status { get; set; }
    }
}
