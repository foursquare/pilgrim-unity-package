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
			EditorWindow.GetWindow(typeof(PilgrimConfigWindow), true, "Pilgrim").Show();
		}

        [MenuItem("Assets/Pilgrim Unity SDK/iOS/Generate AppController.m")]
        private static void GenerateiOSAppController()
        {
            FileGenerator.GenerateiOSAppController();
        }

        [MenuItem("Assets/Pilgrim Unity SDK/Android/Generate App.java")]
        private static void GenerateAndroidAppSubclass()
        {
            FileGenerator.GenerateAndroidAppSubclass();
        }

        [MenuItem("Assets/Pilgrim Unity SDK/Android/Generate AndroidManifest.xml")]
        private static void GenerateAndroidManifest()
        {
            FileGenerator.GenerateAndroidManifest();
        }

    }

}