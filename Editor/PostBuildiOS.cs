#if UNITY_IOS

using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.Networking;

namespace Foursquare
{

    public static class PostBuildiOS
    {

        [PostProcessBuild(0)]
        public static void AddAlwaysAndWhenInUseUsageDescription(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS || !PilgrimConfigSettings.GetBool(PilgrimConfigSettings.CopyWhenInUseToAlwaysKey, true))
            {
                return;
            }
            var plistPath = string.Format("{0}/Info.plist", pathToBuiltProject);

            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            var root = plist.root;

            var whenInUseUsageDescription = root["NSLocationWhenInUseUsageDescription"];
            if (whenInUseUsageDescription != null)
            {
                root["NSLocationAlwaysAndWhenInUseUsageDescription"] = whenInUseUsageDescription;
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

            var pbxProj = new PBXProject();
            pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var targetGuid = pbxProj.GetUnityFrameworkTargetGuid();

            if (!pbxProj.ContainsFramework(targetGuid, "CoreLocation.framework"))
            {
                pbxProj.AddFrameworkToProject(targetGuid, "CoreLocation.framework", false);
            }

            pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

    }

}

#endif
