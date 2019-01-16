using UnityEditor;
using UnityEngine;

namespace Foursquare 
{

	public class PilgrimConfigWindow : EditorWindow 
	{

		private string _consumerKey;

		private string _consumerSecret;

		private bool _copyAlwaysEnabled;

		void OnEnable()
		{
			_consumerKey = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerKeyKey);
			_consumerSecret = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerSecretKey);
			_copyAlwaysEnabled = PilgrimConfigSettings.GetBool(PilgrimConfigSettings.CopyWhenInUseToAlwaysKey, true);
		}

		void OnGUI()
		{
			GUILayout.Label("Settings", EditorStyles.boldLabel);

			GUILayout.BeginVertical();

			GUILayout.Label("Consumer Key");
			_consumerKey = EditorGUILayout.TextField(_consumerKey);

			GUILayout.Label("Consumer Secret");
			_consumerSecret = EditorGUILayout.TextField(_consumerSecret);

			GUILayout.Space(10.0f);

			_copyAlwaysEnabled = GUILayout.Toggle(_copyAlwaysEnabled, " iOS: Copy NSLocationWhenInUseUsageDescription to\n        NSLocationAlwaysAndWhenInUseUsageDescription");

			GUILayout.Space(10.0f);

			if (GUILayout.Button("Save")) {
				PilgrimConfigSettings.Set(PilgrimConfigSettings.ConsumerKeyKey, _consumerKey);
				PilgrimConfigSettings.Set(PilgrimConfigSettings.ConsumerSecretKey, _consumerSecret);
				PilgrimConfigSettings.Set(PilgrimConfigSettings.CopyWhenInUseToAlwaysKey, _copyAlwaysEnabled);
				PilgrimConfigSettings.Save();
				Close();
			}

			GUILayout.EndVertical();
		}

	}

}
