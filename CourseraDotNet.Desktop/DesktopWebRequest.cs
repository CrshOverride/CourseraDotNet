using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Desktop
{
    public class DesktopWebRequest : IWebRequest
    {
        private readonly HttpWebRequest _request;

        public DesktopWebRequest(HttpWebRequest request)
        {
            _request = request;
        }

        public Task<Stream> GetRequestStreamAsync()
        {
            return Task<Stream>.Factory.FromAsync(_request.BeginGetRequestStream, _request.EndGetRequestStream, _request);
        }

        public async Task SetContentAsync(string content)
        {
            using (var stream = await Task<Stream>.Factory.FromAsync(_request.BeginGetRequestStream, _request.EndGetRequestStream, _request).ConfigureAwait(false))
            {
                var bytes = content.ToAscii();
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public async Task<IWebResponse> GetResponseAsync()
        {
            var response = await Task<WebResponse>.Factory.FromAsync(_request.BeginGetResponse, _request.EndGetResponse, _request);
            return new DesktopWebResponse(response);
        }

        public Uri RequestUri
        {
            get { return _request.RequestUri; }
        }

        public string ContentType
        {
            get { return _request.ContentType; }
            set { _request.ContentType = value; }
        }

        public string Method
        {
            get { return _request.Method; }
            set { _request.Method = value; }
        }

        public void Dispose()
        {
            
        }
    }
}
