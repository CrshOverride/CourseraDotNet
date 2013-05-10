using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseraDotNet.Core.Core
{
    public static class StringExtensions
    {
        public static byte[] ToAscii(this string str)
        {
            var retval = new byte[str.Length];
            for (var ix = 0; ix < str.Length; ++ix)
            {
                var ch = str[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }
    }
}
