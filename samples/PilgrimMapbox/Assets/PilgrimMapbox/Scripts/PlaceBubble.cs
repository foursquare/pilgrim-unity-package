using Foursquare;
using System;
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
            if (value == null)
            {
                _loadingText.text = "Pilgrim is determining your location...";
                _venueNameText.text = "";
                _address1Text.text = "";
                _address2Text.text = "";
                _categoryIconRenderer.gameObject.SetActive(false);
                return;
            }

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

    public Exception Exception
    {
        set
        {
            _loadingText.text = value.Message;
            _venueNameText.text = "";
            _address1Text.text = "";
            _address2Text.text = "";
            _categoryIconRenderer.gameObject.SetActive(false);
        }
    }

    public TMP_Text _loadingText;

    public TMP_Text _venueNameText;

    public TMP_Text _address1Text;

    public TMP_Text _address2Text;

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
                _categoryIconRenderer.gameObject.SetActive(true);
                _categoryIconRenderer.material.color = Color.white;
                _categoryIconRenderer.material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
    }

}
