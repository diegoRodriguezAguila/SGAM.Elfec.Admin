using Newtonsoft.Json;
using SGAM.Elfec.Model.Enums;
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
        public ApiStatus Status { get; set; }
    }
}
