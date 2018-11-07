#if UNITY_IOS

using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Foursquare
{
    public class iOSBuildPostprocessor 
    {

        [PostProcessBuildAttribute]
        public static void AddCredentialsToInfoPlist(BuildTarget target, string pathToBuiltProject) 
        {
            var path = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(path));

            PlistElementDict rootDict = plist.root;

            rootDict.SetString("Pilgrim_ConsumerKey", PilgrimConfigSettings.Get("ConsumerKey"));
            rootDict.SetString("Pilgrim_ConsumerSecret", PilgrimConfigSettings.Get("ConsumerSecret"));

            File.WriteAllText(path, plist.WriteToString());
        }

    }
}

#endif
