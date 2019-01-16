using Foursquare;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class PlaceBubble : MonoBehaviour
{

    public Venue Venue
    {
        set
        {
            _loadingText.text = "";
            _venueNameText.text = value.Name;
            var location = value.LocationInformation;
            if (location != null)
            {
                _address1Text.text = location.Address;
                _address2Text.text = string.Format("{0}, {1} {2}", location.City, location.State, location.PostalCode);
            }
            else
            {
                _address1Text.text = "No Address Information";
            }
            StartCoroutine(LoadCategoryIcon(value.Categories[0].Icon));
        }
    }

    [SerializeField]
    private TMP_Text _loadingText;

    [SerializeField]
    private TMP_Text _venueNameText;

    [SerializeField]
    private TMP_Text _address1Text;

    [SerializeField]
    private TMP_Text _address2Text;

    private Renderer _categoryIconRenderer;

    void Awake()
    {
        _categoryIconRenderer = transform.Find("CategoryIcon").GetComponent<Renderer>();
        _categoryIconRenderer.material.mainTexture = null;
    }

    private IEnumerator LoadCategoryIcon(CategoryIcon categoryIcon)
    {
        using (var www = UnityWebRequestTexture.GetTexture(string.Format("{0}88{1}", categoryIcon.Prefix, categoryIcon.Suffix)))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                _categoryIconRenderer.material.color = Color.white;
                _categoryIconRenderer.material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
    }

}
