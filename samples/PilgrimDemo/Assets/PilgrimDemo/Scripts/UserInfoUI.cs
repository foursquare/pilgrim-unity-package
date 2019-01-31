using Foursquare;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{

    [SerializeField]
    private Toggle _userIdToggle;

    [SerializeField]
    private InputField _userIdInputField;

    [SerializeField]
    private Toggle _genderToggle;

    [SerializeField]
    private Dropdown _genderDropdown;

    [SerializeField]
    private Toggle _birthdayToggle;

    [SerializeField]
    private InputField _yearInputField;

    [SerializeField]
    private Dropdown _monthDropDown;

    [SerializeField]
    private Dropdown _dayDropDown;

    [SerializeField]
    private UserInfoCell _userInfoCellPrefab;

    [SerializeField]
    private ScrollRect _scrollRect;

    private int year = 0;
    private int month = 0;
    private int day = 0;

    void Start()
    {
        var keysString = PlayerPrefs.GetString("keys");
        if (keysString != null && keysString.Length > 0)
        {
            var keys = keysString.Split(',');
            foreach (var key in keys)
            {
                var value = PlayerPrefs.GetString(key);
                if (key == "userId")
                {
                    _userIdToggle.isOn = true;
                    _userIdInputField.interactable = true;
                    _userIdInputField.text = value;
                }
                else if (key == "gender")
                {
                    _genderToggle.isOn = true;
                    _genderDropdown.interactable = true;
                    _genderDropdown.value = value == "male" ? 1 : 2;
                }
                else if (key == "birthday")
                {
                    _birthdayToggle.isOn = true;

                    var seconds = long.Parse(value);
#if UNITY_ANDROID
                    seconds /= 1000; // Android uses milliseconds
#endif
                    var epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                    var birthday = epochStart.AddSeconds(seconds);

                    _yearInputField.interactable = true;
                    _yearInputField.text = string.Format("{0}", birthday.Year);
                    _monthDropDown.interactable = true;
                    _monthDropDown.value = birthday.Month - 1;
                    _dayDropDown.interactable = true;
                    _dayDropDown.value = birthday.Day - 1;
                }
                else
                {
                    var userInfoCell = Instantiate(_userInfoCellPrefab, Vector3.zero, Quaternion.identity);
                    userInfoCell.transform.SetParent(_scrollRect.content, false);
                    userInfoCell.transform.SetAsFirstSibling();

                    userInfoCell.Key = key;
                    userInfoCell.Value = value;
                }
            }
        }
    }

    public void OnPressClose()
    {
        var userInfo = new UserInfo();
        if (_userIdInputField.text != null && _userIdInputField.text.Length > 0)
        {
            userInfo.SetUserId(_userIdInputField.text);
        }
        if (year > 0 && month > 0 && day > 0)
        {
            userInfo.SetBirthday(new DateTime(year, month, day));
        }
        if (_genderDropdown.value == 1)
        {
            userInfo.SetGender(UserInfo.Gender.Male);
        }
        else if (_genderDropdown.value == 2)
        {
            userInfo.SetGender(UserInfo.Gender.Female);
        }

        var userInfoCells = FindObjectsOfType<UserInfoCell>();
        foreach (var userInfoCell in userInfoCells)
        {
            var key = userInfoCell.Key;
            var value = userInfoCell.Value;
            if (key != null && value != null & key.Length > 0 && value.Length > 0)
            {
                userInfo.Set(key, value);
            }
        }

        PilgrimUnitySDK.SetUserInfo(userInfo);
        SaveUserInfoToPlayerPrefs(userInfo);
        Destroy(gameObject);
    }

    public void OnCheckUserIdEnabled(bool isOn)
    {
        _userIdInputField.interactable = isOn;
        if (!isOn)
        {
            _userIdInputField.text = null;
        }
    }

    public void OnCheckGenderEnabled(bool isOn)
    {
        _genderDropdown.interactable = isOn;
        if (!isOn)
        {
            _genderDropdown.value = 0;
        }
    }

    public void OnCheckBirthdayEnabled(bool isOn)
    {
        if (isOn)
        {
            _yearInputField.interactable = isOn;
        }
        else
        {
            _yearInputField.interactable = false;
            _yearInputField.text = null;
            year = 0;
            _monthDropDown.interactable = false;
            _monthDropDown.ClearOptions();
            month = 0;
            _monthDropDown.AddOptions(new List<string>(new string[] { "Month" }));
            _dayDropDown.interactable = false;
            _dayDropDown.ClearOptions();
            _dayDropDown.AddOptions(new List<string>(new string[] { "Day" }));
            day = 0;
        }
    }

    public void OnYearEntered()
    {
        var input = _yearInputField.text;
        if (input.Length == 4 && int.TryParse(input, out year))
        {
            UpdateMonthsDropdown();
            month = 1;
            day = 1;
            UpdateDaysDropdown();
        }
        else
        {
            _monthDropDown.interactable = false;
            _monthDropDown.ClearOptions();
            month = 0;
            _monthDropDown.AddOptions(new List<string>(new string[] { "Month" }));
            _dayDropDown.interactable = false;
            _dayDropDown.ClearOptions();
            _dayDropDown.AddOptions(new List<string>(new string[] { "Day" }));
            day = 0;
        }
    }

    public void OnMonthSelected()
    {
        UpdateDaysDropdown();
    }

    public void OnDaySelected()
    {
        day = _dayDropDown.value + 1;
    }

    public void OnPressAddUserInfo()
    {
        var userInfoCell = Instantiate(_userInfoCellPrefab, Vector3.zero, Quaternion.identity);
        userInfoCell.transform.SetParent(_scrollRect.content, false);
        userInfoCell.transform.SetAsFirstSibling();
    }

    private void UpdateMonthsDropdown()
    {
        var cultureInfo = new CultureInfo("en-us");
        string[] monthNames = cultureInfo.DateTimeFormat.MonthNames;
        if (monthNames.Length > 12)
        {// 13 for some reason, last value is empty?!?
            Array.Resize(ref monthNames, monthNames.Length - (monthNames.Length - 12));
        }
        _monthDropDown.ClearOptions();
        _monthDropDown.AddOptions(new List<string>(monthNames));
        _monthDropDown.interactable = true;
    }

    private void UpdateDaysDropdown()
    {
        month = _monthDropDown.value + 1;
        int daysInMonth = DateTime.DaysInMonth(year, month);
        List<string> days = new List<string>();
        for (int i = 1; i <= daysInMonth; i++)
        {
            days.Add(string.Format("{0}", i));
        }
        _dayDropDown.ClearOptions();
        _dayDropDown.AddOptions(days);
        _dayDropDown.interactable = true;
    }

    private void SaveUserInfoToPlayerPrefs(UserInfo userInfo)
    {
        PlayerPrefs.DeleteAll();

        string keysString = "";
        foreach (var pair in userInfo.BackingStore)
        {
            if (keysString.Length > 0)
            {
                keysString += ",";
            }
            keysString += pair.Key;
            PlayerPrefs.SetString(pair.Key, pair.Value);
        }
        PlayerPrefs.SetString("keys", keysString);
        PlayerPrefs.Save();
    }

}
