using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Android
{
    public class AndroidRequestBuilder : IWebRequestBuilder
    {
        public IWebRequest Build(string url, IEnumerable<KeyValuePair<string, string>> headers, CookieContainer cookieContainer)
        {
            var request = WebRequest.Create(url);
            var httpRequest = request as HttpWebRequest;
            httpRequest.CookieContainer = cookieContainer;
            foreach (var kvp in headers)
            {
                switch (kvp.Key.ToUpperInvariant())
                {
                    case "HOST":
                        break;
                    case "REFERER":
                        httpRequest.Referer = kvp.Value;
                        break;
                    case "USER-AGENT":
                        httpRequest.UserAgent = kvp.Value;
                        break;
                    case "ACCEPT":
                        httpRequest.Accept = kvp.Value;
                        break;
                    default:
                        httpRequest.Headers.Add(kvp.Key, kvp.Value);
                        break;
                }
            }
            return new AndroidWebRequest(httpRequest);
        }
    }
}
