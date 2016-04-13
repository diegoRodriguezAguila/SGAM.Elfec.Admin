using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SGAM.Elfec.Model
{
    /// <summary>
    /// Describes a policy rule
    /// </summary>
    public class Rule
    {
        public string Id { get; set; }
        public RuleAction Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Exception { get; set; }
        public IList<IEntity> Entities { get; set; }
        public ApiStatus Status { get; set; }

        public string EntitiesString
        {
            get
            {
                return string.Join(", ", Entities.Select(e =>
                        string.Format("{0}({1})", e.Name, e.Details)));
            }
        }
    }
}
