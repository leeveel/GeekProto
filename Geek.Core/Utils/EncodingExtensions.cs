using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geek.Server
{
    public static class EncodingExtensions
    {
        public static int MyGetByteCount(this Encoding encoding, string s, int index, int count)
        {
            var output = 0;
            var end = index + count;
            var charArray = new char[1];
            for (var i = index; i < end; i++)
            {
                charArray[0] = s[i];
                output += Encoding.UTF8.GetByteCount(charArray);
            }
            return output;
        }
    }
}
