#if UNITY_IOS

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

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

        [PostProcessBuild]
        public static void AddRunPathSearchPaths(BuildTarget buildTarget, string pathToBuiltProject)
        {
            var pbxProj = new PBXProject();
            pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var targetName = PBXProject.GetUnityTargetName();
            var targetGuid = pbxProj.TargetGuidByName(targetName);
            pbxProj.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) '@executable_path/Frameworks' '@loader_path/Frameworks'");

            pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

        [PostProcessBuild]
        public static void AddCopyFrameworkBuildPhase(BuildTarget buildTarget, string pathToBuiltProject)
        {
            var pbxProj = new PBXProject();
            pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var targetName = PBXProject.GetUnityTargetName();
            var targetGuid = pbxProj.TargetGuidByName(targetName);

            var phaseGuid = pbxProj.AddCopyFilesBuildPhase(targetGuid, "Copy Pilgrim Framework", "", "10");
            var frameworkGuid = pbxProj.FindFileGuidByProjectPath("Frameworks/PilgrimUnitySDK/Plugins/iOS/Pilgrim.framework");
            pbxProj.AddFileToBuildSection(targetGuid, phaseGuid, frameworkGuid);

            pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var pbxProjContents = File.ReadAllText(PBXProject.GetPBXProjectPath(pathToBuiltProject));
            var pattern = "([0-9A-Z]+) /\\* Pilgrim.framework in Copy Pilgrim Framework \\*/ = {isa = PBXBuildFile; fileRef = ([0-9A-Z]+) /\\* Pilgrim.framework \\*/; };";
            var replacement = "$1 /* Pilgrim.framework in Copy Pilgrim Framework */ = {isa = PBXBuildFile; fileRef = $2 /* Pilgrim.framework */; settings = {ATTRIBUTES = (CodeSignOnCopy, ); }; };";
            var regex = new Regex(pattern);
            var match = regex.Match(pbxProjContents);
            if (match.Success)
            {
                pbxProjContents = regex.Replace(pbxProjContents, replacement);
            }
            else
            {
                Debug.LogError("Error enabling 'Code Sign on Copy' for 'Copy Pilgrim Framework' build phase, enable manually.");
            }

            File.WriteAllText(PBXProject.GetPBXProjectPath(pathToBuiltProject), pbxProjContents);
        }

        [PostProcessBuild]
        public static void EnsureCoreLocationIsAdded(BuildTarget buildTarget, string pathToBuiltProject)
        {
            var pbxProj = new PBXProject();
            pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

            var targetName = PBXProject.GetUnityTargetName();
            var targetGuid = pbxProj.TargetGuidByName(targetName);

            if (!pbxProj.ContainsFramework(targetGuid, "CoreLocation.framework"))
            {
                pbxProj.AddFrameworkToProject(targetGuid, "CoreLocation.framework", false);
            }

            pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
        }

    }

}

#endif
