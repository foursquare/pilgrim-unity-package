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

        [PostProcessBuild(0)]
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

        [PostProcessBuild(1)]
        public static void AddRunPathSearchPaths(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                var pbxProj = new PBXProject();
                pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

                var targetGuid = pbxProj.GetUnityMainTargetGuid();
                pbxProj.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) '@executable_path/Frameworks' '@loader_path/Frameworks'");

                pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
            }
        }

        [PostProcessBuild(2)]
        public static void AddCopyFrameworkBuildPhase(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                var pbxProj = new PBXProject();
                pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

                var targetGuid = pbxProj.GetUnityMainTargetGuid();
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
        }

        [PostProcessBuild(3)]
        public static void AddStripFrameworksBuildPhase(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
#if UNITY_2018_3_OR_NEWER
                var pbxProj = new PBXProject();
                pbxProj.ReadFromFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));

                var targetGuid = pbxProj.GetUnityMainTargetGuid();

                pbxProj.AddShellScriptBuildPhase(targetGuid, "Pilgrim Strip Frameworks", "/bin/sh", "${BUILT_PRODUCTS_DIR}/${FRAMEWORKS_FOLDER_PATH}/Pilgrim.framework/strip-frameworks.sh");
                pbxProj.WriteToFile(PBXProject.GetPBXProjectPath(pathToBuiltProject));
#else
                var buffer = new byte[12];
                new System.Random().NextBytes(buffer);
                var phaseGuid = System.BitConverter.ToString(buffer).Replace("-", "");

                var pbxProjContents = File.ReadAllText(PBXProject.GetPBXProjectPath(pathToBuiltProject));

                var pattern = "/\\* Copy Pilgrim Framework \\*/,";
                var replacement = string.Format("$0 {0} /* Pilgrim Strip Frameworks */,", phaseGuid);
                var regex = new Regex(pattern);
                var match = regex.Match(pbxProjContents);
                if (match.Success)
                {
                    pbxProjContents = regex.Replace(pbxProjContents, replacement);
                }
                else
                {
                    Debug.LogError("Error adding strip frameworks run script build phase, add manually, please see https://developer.foursquare.com/docs/pilgrim-sdk/quickstart#set-up.");
                }

                pattern = "/\\* Begin PBXShellScriptBuildPhase section \\*/";
                replacement = string.Format(@"$0
                {0} /* Pilgrim Strip Frameworks */ = {{
			        isa = PBXShellScriptBuildPhase;
			        buildActionMask = 2147483647;
			        files = (
			        );
			        name = ""Pilgrim Strip Frameworks"";
			        runOnlyForDeploymentPostprocessing = 0;
			        shellPath = /bin/sh;
			        shellScript = ""${{BUILT_PRODUCTS_DIR}}/${{FRAMEWORKS_FOLDER_PATH}}/Pilgrim.framework/strip-frameworks.sh"";
		        }};", phaseGuid);

                regex = new Regex(pattern);
                match = regex.Match(pbxProjContents);
                if (match.Success)
                {
                    pbxProjContents = regex.Replace(pbxProjContents, replacement);
                }
                else
                {
                    Debug.LogError("Error adding strip frameworks run script build phase, add manually, please see https://developer.foursquare.com/docs/pilgrim-sdk/quickstart#set-up.");
                }

                File.WriteAllText(PBXProject.GetPBXProjectPath(pathToBuiltProject), pbxProjContents);
#endif
            }
        }

        [PostProcessBuild(4)]
        public static void EnsureCoreLocationIsAdded(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
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

}

#endif
