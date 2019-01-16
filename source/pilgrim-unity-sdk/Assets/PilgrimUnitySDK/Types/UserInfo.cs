using System;
using System.Collections.Generic;

namespace Foursquare
{

    public class UserInfo
    {

        public enum Gender 
        {
            Male,
            Female,
            NotSpecified
        }

        private static class Constants
        {
            public const string UserID = "userId";
            public const string Gender = "gender";
            public const string Male = "male";
            public const string Female = "female";
            public const string Birthday = "birthday";
        }

        private IDictionary<string, string> _backingStore = new Dictionary<string, string>();

        public IDictionary<string, string> BackingStore { get { return new Dictionary<string, string>(_backingStore); } }

        public void SetUserId(string userId) 
        {
            _backingStore[Constants.UserID] = userId;
        }

        public void SetGender(Gender gender) 
        {
            switch (gender) {
                case Gender.Male:
                    _backingStore[Constants.Gender] = Constants.Male;
                    break;
                case Gender.Female:
                    _backingStore[Constants.Gender] = Constants.Female;
                    break;
                case Gender.NotSpecified:
                    _backingStore.Remove(Constants.Gender);
                    break;
            }
        }

        public void SetBirthday(DateTime birthday)
        {
            #if UNITY_EDITOR
            _backingStore[Constants.Birthday] = birthday.UnixSecondsFromDateTime().ToString();
            #elif UNITY_IOS
            _backingStore[Constants.Birthday] = birthday.UnixSecondsFromDateTime().ToString();
            #elif UNITY_ANDROID
            _backingStore[Constants.Birthday] = birthday.UnixMillisecondsFromDateTime().ToString();
            #endif
        }

        public void Set(string key, string value)
        {
            if (key == Constants.UserID || key == Constants.Gender || key == Constants.Birthday) {
                return;
            }
            _backingStore[key] = value;
        }

    }

}