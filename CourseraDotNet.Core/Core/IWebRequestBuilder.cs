using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CourseraDotNet.Core.Core
{
    public interface IWebRequestBuilder
    {
        IWebRequest Build(string url, IEnumerable<KeyValuePair<string, string>> headers, CookieContainer cookieContainer, bool allowAutoRedirect = true);
    }
}
