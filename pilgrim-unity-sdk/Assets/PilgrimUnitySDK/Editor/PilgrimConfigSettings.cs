using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Foursquare
{

	public static class PilgrimConfigSettings
	{

		private static string PROJECT_SETTINGS_FILE = Path.Combine("ProjectSettings", "PilgrimConfigSettings.xml");

		private static SortedDictionary<string, string> settings;

		private static SortedDictionary<string, string> Settings {
            get {
                LoadIfEmpty();
                return settings;
            }
        }

		private static object classLock = new object();

		public static string Get(string key)
		{
			lock (classLock) {
				return Settings[key];
			}
		}
		
		public static void Set(string key, string value)
		{
			lock (classLock) {
				Settings[key] = value;
			}
		}

		public static void Save()
		{
			lock (classLock) {
                if (settings == null) {
                    return;
                }
                Directory.CreateDirectory(Path.GetDirectoryName(PROJECT_SETTINGS_FILE));
                using (var writer = new XmlTextWriter(new StreamWriter(PROJECT_SETTINGS_FILE)) {
                        Formatting = Formatting.Indented,
                    }) {
                    writer.WriteStartElement("configSettings");
                    foreach (var kv in settings) {
                        writer.WriteStartElement("configSetting");
                        if (!String.IsNullOrEmpty(kv.Key) && !String.IsNullOrEmpty(kv.Value)) {
                            writer.WriteAttributeString("name", kv.Key);
                            writer.WriteAttributeString("value", kv.Value);
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
		}

		private static void Clear() {
            lock (classLock) {
                settings = new SortedDictionary<string, string>();
            }
        }

		private static void LoadIfEmpty() 
		{
            lock (classLock) {
                if (settings == null) {
					Load();
				}
            }
        }

		private static void Load()
		{
			lock (classLock) {
                Clear();
				var document = XDocument.Load(new StreamReader(File.Open(PROJECT_SETTINGS_FILE, FileMode.OpenOrCreate)));
                foreach (var element in document.Root.Elements()) {
					var key = element.Attribute("name").Value;
					var value = element.Attribute("value").Value;
					settings[key] = value;
				}
            }
		}

	}

}