using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    public class Role
    {
        [JsonProperty("Role")]
        public String RoleName { get; set; }
        public String Description { get; set; }
        public IList<Permission> Permissions { get; set; }
        public short Status { get; set; }
    }
}
