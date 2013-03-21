using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseraDotNet.Core.Core
{
    public interface IWebRequest
    {
        IAsyncResult BeginGetRequestStream(AsyncCallback callback, object asyncState);
        Stream EndGetRequestStream(IAsyncResult result);

        IAsyncResult BeginGetResponse(AsyncCallback callback, object asyncState);
        IWebResponse EndGetResponse(IAsyncResult result);

        Uri RequestUri { get; }
        string ContentType { get; set; }
        string Method { get; set; }
        bool AllowAutoRedirect { get; set; }
    }
}
