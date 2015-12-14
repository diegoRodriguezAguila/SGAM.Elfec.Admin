using SGAM.Elfec.Model.Enums;
using System;
using System.Collections.Generic;

namespace SGAM.Elfec.Model
{
    public class Application
    {
        public string Name { get; set; }
        public string Package { get; set; }
        public string LatestVersion { get; set; }
        public int LatestVersionCode { get; set; }
        public Uri Url { get; set; }
        public Uri IconUrl { get; set; }
        public IList<AppVersion> AppVersions { get; set; }

        public ApiStatus Status { get; set; }

    }
}
