using System;
using System.Collections.Generic;

namespace Utilities
{
    /// <summary>
    ///     Comparer for comparing two keys, handling equality as beeing greater
    ///     Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
    ///     See https://stackoverflow.com/a/21886340
    ///     Caveats apply
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class DuplicateKeyComparer<TKey> : IComparer<TKey> 
        where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            var result = x.CompareTo(y);
            return result == 0 ? 1 : result;
        }
    }
}
