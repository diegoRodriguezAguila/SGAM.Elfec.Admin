using SGAM.Elfec.Model.Enums;
using System;

namespace SGAM.Elfec.Model
{
    public class AppVersion
    {
        public string Version { get; set; }
        public int VersionCode { get; set; }
        public Uri Url { get; set; }
        public Uri IconUrl { get; set; }

        public ApiStatus Status { get; set; }
    }
}
