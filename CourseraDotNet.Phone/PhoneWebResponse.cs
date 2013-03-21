using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Phone
{
    public class PhoneWebResponse : IWebResponse
    {
        private readonly WebResponse _response;

        public WebHeaderCollection Headers
        {
            get { return _response.Headers; }
        }

        public PhoneWebResponse(WebResponse response)
        {
            _response = response;
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }
}
