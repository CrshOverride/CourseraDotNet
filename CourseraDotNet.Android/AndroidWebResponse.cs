using System.IO;
using System.Net;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Android
{
    public class AndroidWebResponse : IWebResponse
    {
        private readonly WebResponse _response;

        public WebHeaderCollection Headers
        {
            get { return _response.Headers; }
        }

        public AndroidWebResponse(WebResponse response)
        {
            _response = response;
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }
}