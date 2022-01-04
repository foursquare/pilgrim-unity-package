using UnityEngine;
using UnityEngine.UI;

public class UserInfoCell : MonoBehaviour
{

    public InputField _keyInputField;

    public InputField _valueInputField;

    public string Key { get { return _keyInputField.text; } set { _keyInputField.text = value; } }

    public string Value { get { return _valueInputField.text; } set { _valueInputField.text = value; } }

    public void OnPressRemove()
    {
        Destroy(gameObject);
    }

}
