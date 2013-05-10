using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.WindowsStore
{
    public class WindowsStoreWebRequest : IWebRequest
    {
        private readonly HttpClient _client;
        private readonly HttpRequestMessage _message;
        private string _content;

        public WindowsStoreWebRequest(HttpClient client, HttpRequestMessage message)
        {
            _client = client;
            _message = message;
        }

        public Task SetContentAsync(string content)
        {
            return Task.Factory.StartNew(() => _content = content);
        }

        public async Task<IWebResponse> GetResponseAsync()
        {
            if (!String.IsNullOrWhiteSpace(_content))
            {
                _message.Content = new StringContent(_content);
                _message.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            }

            var result = await _client.SendAsync(_message);
            return new WindowsStoreWebResponse(result);
        }

        public Uri RequestUri
        {
            get { return _message.RequestUri; }
        }

        public string ContentType { get; set; }

        public string Method
        {
            get
            {
                return _message.Method.ToString();
            }
            set
            {
                HttpMethod method;
                switch (value.ToUpperInvariant())
                {
                    case "POST":
                        method = HttpMethod.Post;
                        break;
                    case "PUT":
                        method = HttpMethod.Put;
                        break;
                    case "DELETE":
                        method = HttpMethod.Delete;
                        break;
                    case "GET":
                        method = HttpMethod.Get;
                        break;
                    case "OPTIONS":
                        method = HttpMethod.Options;
                        break;
                    case "HEAD":
                        method = HttpMethod.Head;
                        break;
                    case "TRACE":
                        method = HttpMethod.Trace;
                        break;
                    default:
                        throw new ArgumentException("Invalid HTTP Method supplied.", "value");
                }
                _message.Method = method;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                }
            }
        }

        ~WindowsStoreWebRequest()
        {
            Dispose(false);
        }
    }
}
