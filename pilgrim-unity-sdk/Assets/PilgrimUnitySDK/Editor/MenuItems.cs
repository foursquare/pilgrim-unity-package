using UnityEditor;
using UnityEngine;

namespace Foursquare
{

    [InitializeOnLoad]
    public class MenuItems
    {

        [MenuItem("Assets/Pilgrim Unity SDK/Configuration")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow(typeof(PilgrimConfigWindow));
			window.titleContent = new GUIContent("Pilgrim");
			window.Show();
		}

        [MenuItem("Assets/Pilgrim Unity SDK/iOS/Generate AppController.m")]
        private static void GenerateAppController()
        {
            iOSGenerator.GenerateAppController();
        }

        [MenuItem("Assets/Pilgrim Unity SDK/Android/Generate App.java")]
        private static void GenerateApp()
        {
            AndroidGenerator.GenerateAppSubclass();
        }

        [MenuItem("Assets/Pilgrim Unity SDK/Android/Generate AndroidManifest.xml")]
        private static void GenerateManifest()
        {
            AndroidGenerator.GenerateManifest();    
        }

    }

}