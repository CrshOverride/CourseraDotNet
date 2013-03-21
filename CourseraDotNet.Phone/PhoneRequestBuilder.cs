using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Browser;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Phone
{
    public class PhoneRequestBuilder : IWebRequestBuilder
    {
        public IWebRequest Build(string url, IEnumerable<KeyValuePair<string, string>> headers, CookieContainer cookieContainer)
        {
            var request = WebRequestCreator.ClientHttp.Create(new Uri(url));
            var httpRequest = request as HttpWebRequest;
            httpRequest.CookieContainer = cookieContainer;
            foreach (var kvp in headers)
            {
                switch (kvp.Key.ToUpperInvariant())
                {
                    case "USER-AGENT":
                        httpRequest.UserAgent =
                            @"Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.97 Safari/537.22";
                        break;
                    case "ACCEPT":
                        httpRequest.Accept = kvp.Value;
                        break;
                    default:
                        httpRequest.Headers[kvp.Key] = kvp.Value;
                        break;
                }
            }
            return new PhoneWebRequest(httpRequest);
        }
    }
}
