using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
