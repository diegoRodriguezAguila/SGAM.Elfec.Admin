using SGAM.Elfec.Model.Enums;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    public class Policy
    {
        public PolicyType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Rule> Rules { get; set; }
        public ApiStatus Status { get; set; }

    }
}
