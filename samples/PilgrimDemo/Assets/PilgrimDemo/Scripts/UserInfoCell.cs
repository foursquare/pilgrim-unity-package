using UnityEngine;
using UnityEngine.UI;

public class UserInfoCell : MonoBehaviour
{

    [SerializeField]
    private InputField _keyInputField;

    [SerializeField]
    private InputField _valueInputField;

    public string Key { get { return _keyInputField.text; } set { _keyInputField.text = value; } }

    public string Value { get { return _valueInputField.text; } set { _valueInputField.text = value; } }

    public void OnPressRemove()
    {
        Destroy(gameObject);
    }

}
