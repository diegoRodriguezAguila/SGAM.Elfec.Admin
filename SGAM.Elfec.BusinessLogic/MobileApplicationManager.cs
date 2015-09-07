using SGAM.Elfec.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGAM.Elfec.BusinessLogic
{
    public static class MobileApplicationManager
    {
        public static ICollection<int> RegisterApplication(MobileApplication newApplication)
        {
            List<int> dasd = new List<int>();
            return null;
        }

        public static ICollection<MobileApplication> GetAllApplications()
        {
            ICollection<MobileApplication> allApplications = new List<MobileApplication>();
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "Nombre de APP Elfec lect LARGO", Version = new Version("1.0.17.140923"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2 Cortes", Version = new Version("2.0.0.7"), PackageName = "com.elfec.lecturas.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec 2", Version = new Version("2.0.0"), PackageName = "com.elfec.app2" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            allApplications.Add(new MobileApplication { Name = "APP Elfec", Version = new Version("1.0.0"), PackageName = "com.elfec.app" });
            return allApplications;
        }
    }
}
