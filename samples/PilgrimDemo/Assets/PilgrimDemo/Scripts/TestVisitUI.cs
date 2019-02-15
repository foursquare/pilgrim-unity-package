using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class TestVisitUI : MonoBehaviour
{

    public InputField _latitudeInputField;
    
    public InputField _longtiudeInputField;

    public void OnPressFireTestVisit() 
    {
        double latitude = 0.0;
        double longitude = 0.0;
        
        if (double.TryParse(_latitudeInputField.text, out latitude) && double.TryParse(_longtiudeInputField.text, out longitude)) 
        {
            PilgrimUnitySDK.FireTestVisit(new Location(latitude, longitude));
        }
        Destroy(gameObject);
    }
   
}
