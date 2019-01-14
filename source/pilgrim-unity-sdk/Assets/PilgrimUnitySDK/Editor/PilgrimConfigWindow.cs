using UnityEditor;
using UnityEngine;

namespace Foursquare 
{

	public class PilgrimConfigWindow : EditorWindow 
	{

		private string _consumerKey;
		private string _consumerSecret;

		void OnEnable()
		{
			_consumerKey = PilgrimConfigSettings.Get("ConsumerKey");
			_consumerSecret = PilgrimConfigSettings.Get("ConsumerSecret");
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

			if (GUILayout.Button("Save")) {
				PilgrimConfigSettings.Set("ConsumerKey", _consumerKey);
				PilgrimConfigSettings.Set("ConsumerSecret", _consumerSecret);
				PilgrimConfigSettings.Save();
				Close();
			}

			GUILayout.EndVertical();
		}

	}

}
