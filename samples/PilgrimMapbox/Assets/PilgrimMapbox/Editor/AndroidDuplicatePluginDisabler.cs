#if UNITY_ANDROID

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AndroidDuplicatePluginDisabler : IPreprocessBuildWithReport
{

    public int callbackOrder { get { return 0; } }

    private class AndroidPlugin
    {
        public string _name;
        public int _major;
        public int _minor;
        public int _patch;
        public PluginImporter _importer;

        public AndroidPlugin(string name, int major, int minor, int patch, PluginImporter importer)
        {
            _name = name;
            _major = major;
            _minor = minor;
            _patch = patch;
            _importer = importer;
        }
    }

    private IDictionary<string, AndroidPlugin> _enabledPluginsMap = new Dictionary<string, AndroidPlugin>();

    private IList<AndroidPlugin> _disabledPlugins = new List<AndroidPlugin>();

    public void OnPreprocessBuild(BuildReport report)
    {
        var pluginImporters = PluginImporter.GetImporters(BuildTarget.Android);
        foreach (var pluginImporter in pluginImporters)
        {
            if (!pluginImporter.isNativePlugin)
            {
                continue;
            }

            var pluginFileName = Path.GetFileName(pluginImporter.assetPath);

            var pattern = "([\\w\\d-]+)-(\\d+)\\.(\\d+)\\.?(\\d?)\\.(?:aar|jar)";
            var regex = new Regex(pattern);
            var match = regex.Match(pluginFileName);
            if (match.Success)
            {
                var groups = match.Groups;
                var pluginName = groups[1].Captures[0].Value;

                var pluginMajorVersion = 0;
                if (!int.TryParse(groups[2].Captures[0].Value, out pluginMajorVersion))
                {
                    Debug.Log(string.Format("Error parsing version for plugin: {0}", pluginFileName));
                    continue;
                }

                var pluginMinorVersion = 0;
                if (!int.TryParse(groups[3].Captures[0].Value, out pluginMinorVersion))
                {
                    Debug.Log(string.Format("Error parsing version for plugin: {0}", pluginFileName));
                    continue;
                }

                var pluginPatchVersion = 0;
                if (groups.Count == 4 && !int.TryParse(groups[4].Captures[0].Value, out pluginPatchVersion))
                {
                    Debug.Log(string.Format("Error parsing version for plugin: {0}", pluginFileName));
                    continue;
                }

                var version = new AndroidPlugin(pluginName, pluginMajorVersion, pluginMinorVersion, pluginPatchVersion, pluginImporter);

                if (_enabledPluginsMap.ContainsKey(pluginName))
                {
                    var enabledVersion = _enabledPluginsMap[pluginName];
                    if (version._major >= enabledVersion._major)
                    {
                        if (version._major > enabledVersion._major)
                        {
                            if (version._minor >= enabledVersion._minor)
                            {
                                if (version._minor > enabledVersion._minor)
                                {
                                    if (version._patch >= enabledVersion._patch)
                                    {
                                        if (version._patch > enabledVersion._patch)
                                        {
                                            _enabledPluginsMap[pluginName] = version;
                                            _disabledPlugins.Add(enabledVersion);
                                        }
                                        else
                                        {
                                            _disabledPlugins.Add(version);
                                        }
                                    }
                                    else
                                    {
                                        _enabledPluginsMap[pluginName] = version;
                                        _disabledPlugins.Add(enabledVersion);
                                    }
                                }
                                else
                                {
                                    _disabledPlugins.Add(version);
                                }
                            }
                            else
                            {
                                _enabledPluginsMap[pluginName] = version;
                                _disabledPlugins.Add(enabledVersion);
                            }
                        }
                        else
                        {
                            _disabledPlugins.Add(version);
                        }
                    }
                    else
                    {
                        _disabledPlugins.Add(version);
                    }
                }
                else
                {
                    _enabledPluginsMap[pluginName] = version;
                }
            }
            else
            {
                continue;
            }
        }

        foreach (var kvp in _enabledPluginsMap)
        {
            var plugin = kvp.Value;
            Debug.Log(string.Format("Enabled {0} {1}.{2}.{3}", plugin._name, plugin._major, plugin._minor, plugin._patch));
            plugin._importer.SetCompatibleWithPlatform(BuildTarget.Android, true);
        }

        foreach (var plugin in _disabledPlugins)
        {
            Debug.Log(string.Format("Disabled {0} {1}.{2}.{3}", plugin._name, plugin._major, plugin._minor, plugin._patch));
            plugin._importer.SetCompatibleWithPlatform(BuildTarget.Android, false);
        }
    }

}

#endif
