using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utilities
{
    public static class Files
    {
        public static IEnumerable<T> ReadFileToCollection<T>(string path)
        {
            return File.ReadLines(path)
                .Select(line => Convert.ChangeType(line, typeof (T)))
                .Cast<T>();
        } 
    }
}
