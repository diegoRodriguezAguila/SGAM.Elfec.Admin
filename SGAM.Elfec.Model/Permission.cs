using SGAM.Elfec.Model.Enums;
using System;

namespace SGAM.Elfec.Model
{
    public class Permission
    {
        public static readonly Permission ADMIN_ACCESS = new Permission
        {
            Name = "admin_app_access",
            Description = "Acceso al sistema de gestión de aplicaciones móviles",
            Status = ApiStatus.Enabled
        };

        public String Name { get; set; }
        public String Description { get; set; }
        public ApiStatus Status { get; set; }
    }
}
