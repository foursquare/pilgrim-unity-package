using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Foursquare
{

    public static class PilgrimConfigSettings
    {

        public const string DidShowConfigOnLoad = "DidShowConfigOnLoad";
        public const string ConsumerKeyKey = "ConsumerKey";
        public const string ConsumerSecretKey = "ConsumerSecret";
        public const string CopyWhenInUseToAlwaysKey = "CopyWhenInUseToAlways";

        private static string PROJECT_SETTINGS_FILE = Path.Combine("ProjectSettings", "PilgrimConfigSettings.xml");

        private static SortedDictionary<string, string> _settings;

        private static SortedDictionary<string, string> Settings
        {
            get
            {
                LoadIfEmpty();
                return _settings;
            }
        }

        private static object _aLock = new object();

        public static string GetString(string key)
        {
            lock (_aLock)
            {
                return Settings.ContainsKey(key) ? Settings[key] : null;
            }
        }

        public static bool GetBool(string key, bool defaultValue)
        {
            lock (_aLock)
            {
                var value = GetString(key);
                return value != null ? value == "true" : defaultValue;
            }
        }

        public static void Set(string key, string value)
        {
            lock (_aLock)
            {
                if (value != null && value.Length > 0)
                {
                    Settings[key] = value;
                }
                else
                {
                    Settings.Remove(key);
                }
            }
        }

        public static void Set(string key, bool value)
        {
            lock (_aLock)
            {
                Settings[key] = value ? "true" : "false";
            }
        }

        public static void Save()
        {
            lock (_aLock)
            {
                if (_settings == null)
                {
                    return;
                }
                if (!File.Exists(PROJECT_SETTINGS_FILE))
                {
                    File.CreateText(PROJECT_SETTINGS_FILE);
                }
                else
                {
                    File.Delete(PROJECT_SETTINGS_FILE);
                }
                using (var writer = new XmlTextWriter(new StreamWriter(PROJECT_SETTINGS_FILE))
                {
                    Formatting = Formatting.Indented,
                })
                {
                    writer.WriteStartElement("configSettings");
                    foreach (var kv in _settings)
                    {
                        writer.WriteStartElement("configSetting");
                        if (!String.IsNullOrEmpty(kv.Key) && !String.IsNullOrEmpty(kv.Value))
                        {
                            writer.WriteAttributeString("name", kv.Key);
                            writer.WriteAttributeString("value", kv.Value);
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
        }

        private static void Clear()
        {
            lock (_aLock)
            {
                _settings = new SortedDictionary<string, string>();
            }
        }

        private static void LoadIfEmpty()
        {
            lock (_aLock)
            {
                if (_settings == null)
                {
                    Load();
                }
            }
        }

        private static void Load()
        {
            lock (_aLock)
            {
                Clear();
                try
                {
                    var document = XDocument.Load(new StreamReader(File.Open(PROJECT_SETTINGS_FILE, FileMode.OpenOrCreate)));
                    foreach (var element in document.Root.Elements())
                    {
                        var key = element.Attribute("name").Value;
                        var value = element.Attribute("value").Value;
                        _settings[key] = value;
                    }
                }
                catch { }
            }
        }

    }

}
