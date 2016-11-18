
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using ApkParser;

namespace SGAM.Elfec.BusinessLogic
{
    /// <summary>
    /// Manager para Apks
    /// </summary>
    public class ApkManager
    {
        public string ApkFilePath { get; }

        public ApkManager(string apkFilePath)
        {
            ApkFilePath = apkFilePath;
        }

        public IObservable<Application> GetApplication()
        {
            return Observable.Start(GetApplicationSync);
        }

        /// <summary>
        /// Obtiene el Model de Aplicación a partir del análisis de un apk
        /// </summary>
        /// <returns></returns>
        private Application GetApplicationSync()
        {
            ApkInfo apkInfo = ApkParser.ApkParser.Parse(ApkFilePath);
            var app = new Application()
            {
                Name = apkInfo.Label,
                Package = apkInfo.PackageName,
                Status = ApiStatus.Enabled,
                LatestVersion = apkInfo.VersionName,
                LatestVersionCode = (int) apkInfo.VersionCode,
                IconUrl = apkInfo.Icons.Last(),
                AppVersions = new List<AppVersion>()
                {
                    new AppVersion()
                    {
                        Version = apkInfo.VersionName,
                        VersionCode = Convert.ToInt32(apkInfo.VersionCode),
                        IconUrl = apkInfo.Icons.Last(),
                        Status = ApiStatus.Enabled
                    }
                }
            };
            return app;
        }
    }
}
