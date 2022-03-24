using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tool.Logic
{
    public static class Tools
    {
        public static string Enumerable2String<T>(this IEnumerable<T> list)
        {
            return "[" + string.Join(",", list) + "]";
        }

        public static void CreateAsDirectory(this string path, bool isFile = false)
        {
            if (isFile)
                path = Path.GetDirectoryName(path);
            if (!Directory.Exists(path))
            {
                CreateAsDirectory(path, true);
                Directory.CreateDirectory(path);
            }
        }

    }
}
