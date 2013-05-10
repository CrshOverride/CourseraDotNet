using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.WindowsStore
{
    public class WindowsStoreRequestBuilder : IWebRequestBuilder
    {
        public IWebRequest Build(string url, IEnumerable<KeyValuePair<string, string>> headers, CookieContainer cookieContainer, bool allowAutoRedirect = true)
        {
            var handler = new HttpClientHandler { AllowAutoRedirect = allowAutoRedirect, CookieContainer = cookieContainer };
            var message = new HttpRequestMessage { RequestUri = new Uri(url) };

            var client = new HttpClient(handler);

            foreach (var kvp in headers)
            {
                switch (kvp.Key.ToUpperInvariant())
                {
                    case "HOST":
                        message.Headers.Host = kvp.Value;
                        break;
                    case "REFERER":
                        message.Headers.Referrer = new Uri(kvp.Value);
                        break;
                    default:
                        message.Headers.Add(kvp.Key, kvp.Value);
                        break;
                }
            }

            return new WindowsStoreWebRequest(client, message);
        }
    }
}
