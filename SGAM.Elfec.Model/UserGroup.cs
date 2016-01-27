using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Group of users
    /// </summary>
    public class UserGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<User> Members { get; set; }
    }
}
