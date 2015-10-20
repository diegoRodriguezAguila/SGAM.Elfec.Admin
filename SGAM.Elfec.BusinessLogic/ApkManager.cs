using ICSharpCode.SharpZipLib.Zip;
using Iteedee.ApkReader;
using SGAM.Elfec.Model;
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
        private const string MANIFEST_NAME = "androidmanifest.xml";
        private const string RESOURCES_NAME = "resources.arsc";
        public static ApkInfo GetApkInfo(string APKFilePath)
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
                            manifestData = new byte[50 * 1024];
                            using (Stream strm = zipfile.GetInputStream(item))
                            {
                                strm.Read(manifestData, 0, manifestData.Length);
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
        public static Application GetApplication(string APKFilePath)
        {
            ApkInfo apkInfo = GetApkInfo(APKFilePath);
            string iconPath = ExtractFileAndSave(APKFilePath, apkInfo.iconFileName[2],
                BuildTempFilePath(apkInfo.packageName, apkInfo.versionName) + @"/icons");
            return new Application()
            {
                Name = apkInfo.label,
                Package = apkInfo.packageName,
                Status = ApiStatus.Enabled,
                LatestVersion = apkInfo.versionName,
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
        }

        /// <summary>
        /// Extrae y guarda un recurso de un apk
        /// </summary>
        /// <param name="APKFilePath"></param>
        /// <param name="fileResourceLocation"></param>
        /// <param name="FilePathToSave"></param>
        /// <param name="index"></param>
        /// <returns>el path del archivo guardado</returns>
        public static string ExtractFileAndSave(string APKFilePath, string fileResourceLocation, string FilePathToSave)
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
                            string fileLocation = Path.Combine(FilePathToSave, fileResourceLocation.Split(Convert.ToChar(@"/")).Last());
                            new FileInfo(fileLocation).Directory.Create();
                            using (Stream strm = zipfile.GetInputStream(item))
                            using (FileStream output = File.Create(fileLocation))
                            {
                                try
                                {
                                    strm.CopyTo(output);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            return fileLocation;

                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Construye la carpeta temporal de archivos para el apk 
        /// </summary>
        /// <param name="APKFilePath"></param>
        /// <param name="APKVersion"></param>
        /// <returns></returns>
        private static string BuildTempFilePath(string packageName, string version)
        {
            return Path.GetTempPath() + @"\SGAM.Elfec\" + packageName + @"-v." + version;
        }
    }
}
