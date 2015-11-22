using System.Collections.Generic;

namespace Utilities
{
    public static class Dictionary
    {
        public static TValue TryOrGetDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue = default(TValue))
        {
            var value = defaultValue;
            if (dictionary.ContainsKey(key))
            {
                value = dictionary[key];
            }
            return value;
        }
    }
}
