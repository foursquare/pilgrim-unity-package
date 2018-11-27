using Foursquare;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour 
{

	public InputField userIDInputField;

	public Dropdown genderDropdown;

	public InputField yearInputField;
	public Dropdown monthDropDown;
	public Dropdown dayDropDown;

	private int year = 0;
	private int month = 0;
	private int day = 0;

	public void OnPressClose()
	{
		PilgrimUserInfo userInfo = new PilgrimUserInfo();
		if (userIDInputField.text != null && userIDInputField.text.Length > 0) {
			userInfo.SetUserId(userIDInputField.text);
		}
		if (year > 0 && month > 0 && day > 0) {
			userInfo.SetBirthday(new DateTime(year, month, day));
		}
		if (genderDropdown.value == 1) {
			userInfo.SetGender(PilgrimUserInfo.Gender.Male);	
		} else if (genderDropdown.value == 2) {
			userInfo.SetGender(PilgrimUserInfo.Gender.Female);
		}
		PilgrimUnitySDK.SetUserInfo(userInfo);
		Destroy(gameObject);
	}

	public void OnCheckUserIdEnabled(bool isOn) 
	{
		userIDInputField.interactable = isOn;
		if (!isOn) {
			userIDInputField.text = null;
		}
	}

	public void OnCheckGenderEnabled(bool isOn) 
	{
		genderDropdown.interactable = isOn;
		if (!isOn) {
			genderDropdown.value = 0;
		}
	}

	public void OnCheckBirthdayEnabled(bool isOn) 
	{
		if (isOn) {
			yearInputField.interactable = isOn;
		} else {
			yearInputField.interactable = false;
			yearInputField.text = null;
			year = 0;
			monthDropDown.interactable = false;
			monthDropDown.ClearOptions();
			month = 0;
			monthDropDown.AddOptions(new List<string>(new string[] { "Month" }));
			dayDropDown.interactable = false;
			dayDropDown.ClearOptions();
			dayDropDown.AddOptions(new List<string>(new string[] { "Day" }));
			day = 0;
		}
	}

	public void OnYearEntered() 
	{
		var input = yearInputField.text;
		if (input.Length == 4 && int.TryParse(input, out year)) {
			UpdateMonthsDropdown();
			month = 1;
			UpdateDaysDropdown();
		} else {
			monthDropDown.interactable = false;
			monthDropDown.ClearOptions();
			month = 0;
			monthDropDown.AddOptions(new List<string>(new string[] { "Month" }));
			dayDropDown.interactable = false;
			dayDropDown.ClearOptions();
			dayDropDown.AddOptions(new List<string>(new string[] { "Day" }));
			day = 0;
		}
	}

	public void OnMonthSelected() 
	{
		UpdateDaysDropdown();	
	}

	public void OnDaySelected()
	{
		day = dayDropDown.value + 1;
	}

	private void UpdateMonthsDropdown()
	{
		var cultureInfo = new CultureInfo("en-us");
		string[] monthNames = cultureInfo.DateTimeFormat.MonthNames;
		if (monthNames.Length > 12)  {// 13 for some reason, last value is empty?!?
			Array.Resize(ref monthNames, monthNames.Length - (monthNames.Length - 12));
		}
		monthDropDown.ClearOptions();
		monthDropDown.AddOptions(new List<string>(monthNames));
		monthDropDown.interactable = true;
	}

	private void UpdateDaysDropdown()
	{
		month = monthDropDown.value + 1;
		int daysInMonth = DateTime.DaysInMonth(year, month);
		List<string> days = new List<string>();
		for (int i = 1; i <= daysInMonth; i++) {
			days.Add(string.Format("{0}", i));
		}
		dayDropDown.ClearOptions();
		dayDropDown.AddOptions(days);
		dayDropDown.interactable = true;
	}

}
