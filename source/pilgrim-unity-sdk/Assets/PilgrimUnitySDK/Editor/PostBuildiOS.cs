#if UNITY_IOS

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Foursquare
{

    public static class PostBuildiOS
    {

        [PostProcessBuild]
        public static void AddAlwaysAndWhenInUseUsageDescription(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS && PilgrimConfigSettings.GetBool(PilgrimConfigSettings.CopyWhenInUseToAlwaysKey, true))
            {
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
        }

        // TODO REMOVE THIS WHEN MODULES ARE REMOVED FROM PILGRIM IN 2.1.1
        [PostProcessBuild]
        public static void EnableModules(BuildTarget buildTarget, string pathToBuiltProject)
        {
            var pbxProj = new PBXProject();
            pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var targetName = PBXProject.GetUnityTargetName();
            var targetGuid = pbxProj.TargetGuidByName(targetName);
            pbxProj.SetBuildProperty(targetGuid, "CLANG_ENABLE_MODULES", "YES");

            pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

    }

}

#endif
