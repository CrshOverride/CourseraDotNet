using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CourseraDotNet.Core.Core
{
    public interface IWebResponse
    {
        WebHeaderCollection Headers { get; }
        Stream GetResponseStream();
    }
}
