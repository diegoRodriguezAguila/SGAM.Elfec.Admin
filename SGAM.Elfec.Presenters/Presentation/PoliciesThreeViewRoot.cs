﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            foreach (var policy in Policies)
            {
                policy.Rules = new ObservableCollection<Rule>(policy.Rules);
            }
        }
        public ObservableCollection<Policy> Policies { get; set; }
    }
}
