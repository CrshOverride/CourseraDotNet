using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Core
{
    public class Session
    {
        private User _user;

        public CookieContainer CookieJar { get; private set; }
        private IWebRequestBuilder RequestBuilder { get; set; }
        public User User { get { return _user; } }
        internal bool IsAuthRedirected { get; set; }

        private Session(IWebRequestBuilder requestBuilder)
        {
            CookieJar = new CookieContainer();
            RequestBuilder = requestBuilder;
            IsAuthRedirected = false;
        }

        public static async Task<Session> LoginAsync(string email, string password, IWebRequestBuilder requestBuilder)
        {
            const string urlLogin = "https://www.coursera.org/maestro/api/user/login";
            var session = new Session(requestBuilder);
            var csrfToken = ConstructCsrfToken();
            var data = string.Format("email_address={0}&password={1}", Uri.EscapeDataString(email), Uri.EscapeDataString(password));
            var headers = CommonHeaders();
            headers.Add("Origin", "https://www.coursera.org");
            headers.Add("X-CSRFToken", csrfToken);
            headers.Add("Host", "www.coursera.org");
            headers.Add("Referer", "https://www.coursera.org/account/signin");

            using (var request = requestBuilder.Build(urlLogin, headers, session.CookieJar))
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                session.CookieJar.Add(request.RequestUri, new Cookie("csrftoken", csrfToken));

                await request.SetContentAsync(data);

                using (var response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    using (var sr = new StreamReader(await response.GetResponseStreamAsync()))
                    {
                        var userJson = sr.ReadToEnd();
                        session._user = JsonConvert.DeserializeObject<User>(userJson);
                    }
                }
            }

            return session;
        }

        public async Task<string> GetPageAsync(string url, HttpVerb verb, string referrer = null)
        {
            var responseText = "";

            using (var request = RequestBuilder.Build(url, CommonHeaders(referrer), CookieJar))
            {
                request.Method = verb.ToVerbString();

                using (var response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    using (var stream = await response.GetResponseStreamAsync())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            responseText = sr.ReadToEnd();
                        }
                    }

                    var doc = new HtmlDocument();
                    doc.LoadHtml(responseText);

                    var scripts = doc.DocumentNode.Descendants("script");
                    foreach (var s in scripts.Where(s => s.InnerText.Contains("top.location")))
                    {
                        var newUrl = s.InnerText.Split('=')[1].Trim().Trim(new[] {' ', ';', '"'});
                        return await GetPageAsync(newUrl, verb).ConfigureAwait(false);
                    }
                }
            }

            return responseText;
        }

        public async Task<Stream> GetVideoAsync(string url, HttpVerb verb)
        {
            using (var request = RequestBuilder.Build(url, CommonHeaders(), CookieJar))
            {
                request.Method = verb.ToVerbString();

                using (var response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    return await response.GetResponseStreamAsync();
                }
            }
        }

        public async Task<string> UnprotectedProtectedAsssetUrl(string url, HttpVerb verb)
        {
            using (var request = RequestBuilder.Build(url, CommonHeaders(), CookieJar, false))
            {
                request.Method = verb.ToVerbString();

                using (var response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    return response.Headers["Location"].First();
                }
            }
        }

        private static string ConstructCsrfToken()
        {
            var sb = new StringBuilder();
            var random = new Random();
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (var i = 0; i < 24; i++)
            {
                sb.Append(chars[random.Next(chars.Length)]);
            }
            return sb.ToString();
        }

        private static Dictionary<string, string> CommonHeaders(string referrer = null)
        {
            var headers = new Dictionary<string, string>
                {
                    { "User-Agent", @"Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.97 Safari/537.22" },
                    { "Accept", "text/html, application/xhtml+xml, */*" }
                };

            if (referrer != null) headers.Add("Referrer", referrer);

            return headers;
        }
    }
}
