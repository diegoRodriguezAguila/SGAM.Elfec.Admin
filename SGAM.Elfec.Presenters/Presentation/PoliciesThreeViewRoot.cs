using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SGAM.Elfec.Model.Presentation
{
    public class PoliciesThreeViewRoot
    {
        public PoliciesThreeViewRoot()
        {

        }
        public PoliciesThreeViewRoot(IEnumerable<Policy> policies)
        {
            Policies = new ObservableCollection<Policy>(policies);
        }
        public ObservableCollection<Policy> Policies { get; set; }
    }
}
