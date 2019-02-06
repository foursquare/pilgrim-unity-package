using System.IO;
using UnityEditor;

namespace Foursquare
{

    public class FrameworkProcessoriOS : AssetPostprocessor
    {

        void OnPreprocessAsset()
        {
            var assetName = Path.GetFileName(assetPath);
            if (assetName != "Pilgrim.framework")
            {
                return;
            }

            var plugin = PluginImporter.GetAtPath(assetPath) as PluginImporter;
            plugin.SetCompatibleWithAnyPlatform(false);
            plugin.SetCompatibleWithPlatform(BuildTarget.iOS, true);
        }

    }

}
