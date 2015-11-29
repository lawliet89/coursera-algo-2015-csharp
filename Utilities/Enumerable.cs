using System.Collections.Generic;

namespace Utilities
{
    public static class Enumerable
    {
        public static IEnumerable<long> Range(long lower, long count)
        {
            for (var i = 0; i < count; ++i)
            {
                yield return lower + i;
            }
        } 
    }
}
