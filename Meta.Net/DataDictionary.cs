using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Meta.Net
{
    /// <summary>
    /// This class is a wrapper for ConcurrentDictionary, however
    /// always deserializes to the libary's default StringComparer which is
    /// StringComparer.InvariantCultureIgnoreCase. For database naming
    /// convensions this is important.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DataDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
    {
        public DataDictionary()
            : base((IEqualityComparer<TKey>) Constants.StringComparer)
        {
            
        }
    }
}
