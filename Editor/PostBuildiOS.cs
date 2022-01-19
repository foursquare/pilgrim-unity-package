#if UNITY_IOS

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

namespace Foursquare
{

    public static class PostBuildiOS
    {

        [PostProcessBuild(0)]
        public static void AddAlwaysAndWhenInUseUsageDescriptionIfNeeded(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }

            var plistPath = string.Format("{0}/Info.plist", pathToBuiltProject);

            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            var root = plist.root;

            var whenInUseUsageDescription = root["NSLocationWhenInUseUsageDescription"]?.AsString() ?? "";
            var alwaysAndwhenInUseUsageDescription = root["NSLocationAlwaysAndWhenInUseUsageDescription"]?.AsString() ?? "";
            if (!String.IsNullOrEmpty(whenInUseUsageDescription) && String.IsNullOrEmpty(alwaysAndwhenInUseUsageDescription))
            {
                root["NSLocationAlwaysAndWhenInUseUsageDescription"] = root["NSLocationWhenInUseUsageDescription"];
            }

            plist.WriteToFile(plistPath);
        }

        [PostProcessBuild(1)]
        public static void EnsureCoreLocationIsAdded(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }

            var project = new PBXProject();
            project.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var targetGuid = project.GetUnityFrameworkTargetGuid();

            if (!project.ContainsFramework(targetGuid, "CoreLocation.framework"))
            {
                project.AddFrameworkToProject(targetGuid, "CoreLocation.framework", false);
            }

            project.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

        [PostProcessBuild(2)]
        public static void CopyFrameworkFromXCFramework(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }

            var sourcePath = pathToBuiltProject + "/Frameworks/com.foursquare.pilgrim.unity/Pilgrim.xcframework";
            var destPath = "Frameworks/Pilgrim.framework";

            var deviceFrameworkPath = "ios-arm64_armv7/Pilgrim.framework";
            var simulatorFrameworkPath = "ios-arm64_i386_x86_64-simulator/Pilgrim.framework";

            if (PlayerSettings.iOS.sdkVersion == iOSSdkVersion.DeviceSDK)
            {
                sourcePath = Path.Combine(sourcePath, deviceFrameworkPath);
            }
            else
            {
                sourcePath = Path.Combine(sourcePath, simulatorFrameworkPath);
            }

            CopyAndReplaceDirectory(sourcePath, Path.Combine(pathToBuiltProject, destPath), new string[] { ".meta" });
            Directory.Delete(Path.Combine(pathToBuiltProject, "Frameworks", "com.foursquare.pilgrim.unity"), true);

            var project = new PBXProject();
            project.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            project.AddFile(destPath, destPath, PBXSourceTree.Source);

            project.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

        [PostProcessBuild(3)]
        public static void EmbedPilgrimInAppTarget(BuildTarget buildTarget, string pathToBuiltProject)
        {
            var project = new PBXProject();
            project.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            string targetGuid = project.GetUnityMainTargetGuid();
            project.SetBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)/Frameworks");

            string fileGuid = project.FindFileGuidByProjectPath("Frameworks/Pilgrim.framework");
            PBXProjectExtensions.AddFileToEmbedFrameworks(project, targetGuid, fileGuid);

            project.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

        [PostProcessBuild(4)]
        public static void AddPilgrimInFrameworkTarget(BuildTarget buildTarget, string pathToBuiltProject)
        {
            var project = new PBXProject();
            project.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            string targetGuid = project.GetUnityFrameworkTargetGuid();
            project.SetBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)/Frameworks");

            project.AddFrameworkToProject(targetGuid, "Pilgrim.framework", false);

            project.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

        private static void CopyAndReplaceDirectory(string srcPath, string dstPath, IList<string> excludeExtensions = null)
        {
            if (Directory.Exists(dstPath))
            {
                Directory.Delete(dstPath);
            }
            if (File.Exists(dstPath))
            {
                File.Delete(dstPath);
            }
            Directory.CreateDirectory(dstPath);

            foreach (var file in Directory.GetFiles(srcPath))
            {
                if (excludeExtensions.Contains(Path.GetExtension(file)))
                {
                    continue;
                }
                File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));
            }
            foreach (var dir in Directory.GetDirectories(srcPath))
            {
                CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)), excludeExtensions);
            }
        }

    }

}

#endif
