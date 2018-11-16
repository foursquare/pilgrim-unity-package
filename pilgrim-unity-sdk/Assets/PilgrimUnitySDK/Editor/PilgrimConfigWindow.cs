using UnityEditor;
using UnityEngine;

namespace Foursquare 
{

	public class PilgrimConfigWindow : EditorWindow 
	{

		private string consumerKey;
		private string consumerSecret;

		void OnEnable()
		{
			consumerKey = PilgrimConfigSettings.Get("ConsumerKey");
			consumerSecret = PilgrimConfigSettings.Get("ConsumerSecret");
		}

		void OnGUI()
		{
			GUILayout.Label("Settings", EditorStyles.boldLabel);

			GUILayout.BeginVertical();

			GUILayout.Label("Consumer Key");
			consumerKey = EditorGUILayout.TextField(consumerKey);

			GUILayout.Label("Consumer Secret");
			consumerSecret = EditorGUILayout.TextField(consumerSecret);

			GUILayout.Space(10.0f);

			var save = GUILayout.Button("Save");
			if (save) {
				PilgrimConfigSettings.Set("ConsumerKey", consumerKey);
				PilgrimConfigSettings.Set("ConsumerSecret", consumerSecret);
				PilgrimConfigSettings.Save();
				Close();
			}

			GUILayout.EndVertical();
		}

	}

}
