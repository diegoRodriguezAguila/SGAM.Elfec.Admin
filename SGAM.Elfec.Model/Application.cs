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
        public string LatestFullVersion { get { return string.Format("{0} ({1})", LatestVersion, LatestVersionCode); } }
        public Uri Url { get; set; }
        public Uri IconUrl { get; set; }
        public IList<AppVersion> AppVersions { get; set; }

        public ApiStatus Status { get; set; }

    }
}
