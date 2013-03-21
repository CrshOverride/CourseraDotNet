using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Desktop
{
    public class DesktopWebResponse : IWebResponse
    {
        private readonly WebResponse _response;

        public WebHeaderCollection Headers
        {
            get { return _response.Headers; }
        }

        public DesktopWebResponse(WebResponse response)
        {
            _response = response;
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }
}
