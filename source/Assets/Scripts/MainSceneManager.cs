using Foursquare;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour 
{

	private enum Mode {
		Visits,
		GeofenceEvents,
		Logs
	}


	private Mode mode;

	private Vector2 scrollPosition = Vector2.zero;

	private Texture2D bgTex;

	void OnGUI()
	{	
		if (!bgTex) {
			bgTex = new Texture2D(1, 1);
			Color fillColor = new Color32(0, 0, 0, 80);
 			Color[] fillColorArray =  bgTex.GetPixels();
 
 			for (var i = 0; i < fillColorArray.Length; i++) {
     			fillColorArray[i] = fillColor;
 			}
  
 			bgTex.SetPixels(fillColorArray);
 			bgTex.Apply();
		}

		GUILayout.BeginVertical();

		GUILayout.Space(20);

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Visits", GUILayout.Height(50.0f))) {
			mode = Mode.Visits;
		}
		if (GUILayout.Button("Geofence Events", GUILayout.Height(50.0f))) {
			mode = Mode.GeofenceEvents;
		}
		if (GUILayout.Button("Logs", GUILayout.Height(50.0f))) {
			mode = Mode.Logs;
		}

		GUILayout.EndHorizontal();

		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width));

		GUILayout.BeginVertical();

		switch (mode) {
			case Mode.Logs:
				OnLogsGUI();
				break;
			default:
				break;
		}

        GUILayout.EndVertical();

		GUILayout.EndScrollView();

		GUILayout.EndVertical();
	}

	private void OnLogsGUI()
	{
		List<Log> logs = PilgrimUnitySDK.GetLogs();
		for (int i = 0; i < logs.Count; i++) {
			Log log = logs[i];
			GUIStyle gs = new GUIStyle();
			if (i % 2 == 0) {
				gs.normal.background = bgTex;
			}
			GUILayout.BeginVertical(gs);
			GUILayout.Label("<size=20>" + log.Title + " @ " + log.Timestamp + "</size>");
			GUILayout.Label("<size=15>" + log.Description + "</size>");
			GUILayout.EndVertical();
		}
	}
	
}
