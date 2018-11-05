using System;
using System.Collections.Generic;

namespace Foursquare
{

    public class PilgrimUserInfo
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

        private IDictionary<string, string> backingStore = new Dictionary<string, string>();

        public IDictionary<string, string> BackingStore { get { return new Dictionary<string, string>(backingStore); } }

        public void SetUserId(string userId) 
        {
            backingStore[Constants.UserID] = userId;
        }

        public void SetGender(Gender gender) 
        {
            switch (gender) {
                case Gender.Male:
                    backingStore[Constants.Gender] = Constants.Male;
                    break;
                case Gender.Female:
                    backingStore[Constants.Gender] = Constants.Female;
                    break;
            }
        }

        public void SetBirthday(DateTime birthday)
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            #if UNITY_EDITOR
            var seconds = (long)(birthday - epochStart).TotalSeconds;
            backingStore[Constants.Birthday] = seconds.ToString();
            #elif UNITY_IOS
            var seconds = (long)(birthday - epochStart).TotalSeconds;
            backingStore[Constants.Birthday] = seconds.ToString();
            #elif UNITY_ANDROID
            var milliseconds = (long)(birthday - epochStart).TotalMilliseconds;
            backingStore[Constants.Birthday] = milliseconds.ToString();
            #endif
        }

        public void Set(string key, string value)
        {
            if (key == Constants.UserID || key == Constants.Gender || key == Constants.Birthday) {
                return;
            }
            backingStore[key] = value;
        }

    }

}