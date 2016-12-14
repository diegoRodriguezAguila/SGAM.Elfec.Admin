using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Model.Interfaces;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Group of users
    /// </summary>
    public class UserGroup : IEntity
    {
        public string EntityType => GetType().Name;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<User> Members { get; set; }

        public UserGroupStatus Status { get; set; }

        public string Details => "Grupo de Usuarios";
    }
}
