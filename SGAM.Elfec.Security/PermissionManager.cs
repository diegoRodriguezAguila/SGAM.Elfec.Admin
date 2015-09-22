using SGAM.Elfec.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Security
{
    public class PermissionManager
    {
        private static WeakReference<PermissionManager> _permissionsManagerInstanceRef;
        /// <summary>
        /// No se puede instanciar esta clase directamente, debe utilizar la propiedad <see cref="Instance"/>
        /// </summary>
        private PermissionManager()
        {
        }

        static PermissionManager()
        {
            _permissionsManagerInstanceRef = new WeakReference<PermissionManager>(null);
        }

        public static PermissionManager Instance
        {
            get
            {
                PermissionManager currentPermisionManager;
                if (!_permissionsManagerInstanceRef.TryGetTarget(out currentPermisionManager))
                {
                    currentPermisionManager = new PermissionManager();
                    _permissionsManagerInstanceRef.SetTarget(currentPermisionManager);
                }
                return currentPermisionManager;
            }
        }

        /// <summary>
        /// Verifica si el usuario tiene acceso de administrador al sistema
        /// </summary>
        /// <param name="user">usuario a verificar</param>
        /// <returns>true si es que tiene el permiso</returns>
        public bool HasAdminAccessPermission(User user)
        {
            return HasPermission(user, Permission.ADMIN_ACCESS);
        }

        /// <summary>
        /// Verifica si el usuario tiene cierto permiso en alguno de sus roles asignados
        /// </summary>
        /// <param name="user">usuario a verificar</param>
        /// <param name="permission">permiso a verificar</param>
        /// <returns>true si es que tiene el permiso</returns>
        public bool HasPermission(User user, Permission permission)
        {
            foreach (var role in user.Roles)
            {
                if (role.Permissions.Any(perm => perm.Name == permission.Name))
                    return true;
            }
            return false;
        }
    }
}
