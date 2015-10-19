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

        public static Application GetApplication(string APKFilePath)
        {
            ApkInfo apkInfo = GetApkInfo(APKFilePath);
            var icons = apkInfo.iconFileName;
            ExtractFileAndSave(APKFilePath, icons[2], new FileInfo(APKFilePath).Name + @"\icons\");
            return new Application()
            {
                Name = apkInfo.label,
                Package = apkInfo.packageName,
                Status = ApiStatus.Enabled,
                LatestVersion = apkInfo.versionName,
                AppVersions = new List<AppVersion>()
                {
                    new AppVersion()
                    {
                        Version = apkInfo.versionName,
                        VersionCode = Convert.ToInt32(apkInfo.versionCode),
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
        public static void ExtractFileAndSave(string APKFilePath, string fileResourceLocation, string FilePathToSave)
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

                        }
                    }
                }
            }
        }
    }
}
