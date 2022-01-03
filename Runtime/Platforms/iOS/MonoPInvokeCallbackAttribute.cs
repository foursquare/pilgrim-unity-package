#if UNITY_IOS

using System;

namespace Foursquare
{

    public sealed class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type type) { }
    }

}

#endif