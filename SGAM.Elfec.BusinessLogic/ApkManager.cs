using ICSharpCode.SharpZipLib.Zip;
using Iteedee.ApkReader;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SGAM.Elfec.BusinessLogic
{
    /// <summary>
    /// Manager para Apks
    /// </summary>
    public class ApkManager
    {
        private const string ICON_DEFAULT = "res/mipmap-xhdpi-v4/ic_launcher.png";
        private const string MANIFEST_NAME = "androidmanifest.xml";
        private const string RESOURCES_NAME = "resources.arsc";
        private const string ICONS_PATH = @"/icons";
        private string _apkFilePath;

        public string APKFilePath { get { return _apkFilePath; } }

        public ApkManager(string APKFilePath)
        {
            _apkFilePath = APKFilePath;
        }


        public ApkInfo GetApkInfo()
        {
            byte[] manifestData = null;
            byte[] resourcesData = null;
            using (ZipInputStream zip = new ZipInputStream(File.OpenRead(APKFilePath)))
            {
                using (var filestream = new FileStream(APKFilePath, FileMode.Open, FileAccess.Read))
                {
                    ZipFile zipfile = new ZipFile(filestream);
                    ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == MANIFEST_NAME)
                        {
                            //manifestData = new byte[50 * 1024];
                            int size = item.Size == -1 ? (50 * 1024) : (int)item.Size;
                            using (Stream strm = zipfile.GetInputStream(item))
                            {
                                using (BinaryReader s = new BinaryReader(strm))
                                {
                                    manifestData = s.ReadBytes(size);

                                }
                                //strm.Read(manifestData, 0, manifestData.Length);
                            }

                        }
                        if (item.Name.ToLower() == RESOURCES_NAME)
                        {
                            using (Stream strm = zipfile.GetInputStream(item))
                            {
                                using (BinaryReader s = new BinaryReader(strm))
                                {
                                    resourcesData = s.ReadBytes((int)item.Size);

                                }
                            }
                        }
                    }
                }
            }
            return new ApkReader().extractInfo(manifestData, resourcesData);
        }

        /// <summary>
        /// Obtiene el Model de Aplicación a partir del análisis de un apk
        /// </summary>
        /// <param name="APKFilePath"></param>
        /// <returns></returns>
        public void GetApplication(ResultCallback<Application> callback)
        {
            try
            {
                ApkInfo apkInfo = GetApkInfo();
                string iconPath = ExtractFileAndSave(BuildApkIconFilename(apkInfo),
                    BuildTempFilePath(apkInfo.packageName, apkInfo.versionName) + ICONS_PATH);
                var app = new Application()
                {
                    Name = apkInfo.label,
                    Package = apkInfo.packageName,
                    Status = ApiStatus.Enabled,
                    LatestVersion = apkInfo.versionName,
                    LatestVersionCode = Convert.ToInt32(apkInfo.versionCode),
                    IconUrl = new Uri(iconPath),
                    AppVersions = new List<AppVersion>()
                {
                    new AppVersion()
                    {
                        Version = apkInfo.versionName,
                        VersionCode = Convert.ToInt32(apkInfo.versionCode),
                        IconUrl = new Uri(iconPath),
                        Status = ApiStatus.Enabled
                    }
                }
                };
                callback.OnSuccess(null, app);
                return;
            }
            catch (Exception e)
            {
                callback.AddErrors(e);
            }
            callback.OnFailure(this);

        }

        /// <summary>
        /// Extrae y guarda un recurso de un apk
        /// </summary>
        /// <param name="fileResourceLocation"></param>
        /// <param name="filePathToSave"></param>
        /// <returns>el path del archivo guardado</returns>
        public string ExtractFileAndSave(string fileResourceLocation, string filePathToSave)
        {
            using (ZipInputStream zip = new ZipInputStream(File.OpenRead(APKFilePath)))
            {
                using (var filestream = new FileStream(APKFilePath, FileMode.Open, FileAccess.Read))
                {
                    ZipFile zipfile = new ZipFile(filestream);
                    ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == fileResourceLocation)
                        {
                            string fileLocation = Path.Combine(filePathToSave, fileResourceLocation.Split(Convert.ToChar(@"/")).Last());
                            new FileInfo(fileLocation).Directory.Create();
                            using (Stream strm = zipfile.GetInputStream(item))
                            using (FileStream output = File.Create(fileLocation))
                            {
                                try
                                {
                                    strm.CopyTo(output);
                                }
                                catch (Exception)
                                {
                                    return null;
                                }
                            }
                            return fileLocation;

                        }
                    }
                }
            }
            return null;
        }

        private string BuildApkIconFilename(ApkInfo apkInfo)
        {
            int iconsCount = apkInfo.iconFileName.Count;
            if (iconsCount == 0)
                return ICON_DEFAULT;
            if (iconsCount < 3)
                return apkInfo.iconFileName[iconsCount - 1];
            return apkInfo.iconFileName[2];
        }

        /// <summary>
        /// Construye la carpeta temporal de archivos para el apk 
        /// </summary>
        /// <param name="APKFilePath"></param>
        /// <param name="APKVersion"></param>
        /// <returns></returns>
        private string BuildTempFilePath(string packageName, string version)
        {
            return Path.GetTempPath() + @"\SGAM.Elfec\" + packageName + @"-v." + version;
        }
    }
}
