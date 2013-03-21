using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Phone
{
    public class PhoneWebRequest : IWebRequest
    {
        private readonly HttpWebRequest _request;

        public PhoneWebRequest(HttpWebRequest request)
        {
            _request = request;
        }

        public IAsyncResult BeginGetRequestStream(AsyncCallback callback, object asyncState)
        {
            return _request.BeginGetRequestStream(callback, asyncState);
        }

        public Stream EndGetRequestStream(IAsyncResult result)
        {
            return _request.EndGetRequestStream(result);
        }

        public IAsyncResult BeginGetResponse(AsyncCallback callback, object asyncState)
        {
            return _request.BeginGetResponse(callback, asyncState);
        }

        public IWebResponse EndGetResponse(IAsyncResult result)
        {
            return new PhoneWebResponse(_request.EndGetResponse(result));
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

        public bool AllowAutoRedirect
        {
            get { return _request.AllowAutoRedirect; }
            set { _request.AllowAutoRedirect = value; }
        }
    }
}
