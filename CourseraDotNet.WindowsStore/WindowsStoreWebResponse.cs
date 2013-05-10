using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.WindowsStore
{
    public class WindowsStoreWebResponse : IWebResponse
    {
        private readonly HttpResponseMessage _response;

        public WindowsStoreWebResponse(HttpResponseMessage response)
        {
            _response = response;
        }

        public IDictionary<string, IEnumerable<string>> Headers
        {
            get
            {
                return _response.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public async Task<Stream> GetResponseStreamAsync()
        {
            return await _response.Content.ReadAsStreamAsync();
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
                _response.Dispose();
            }
        }

        ~WindowsStoreWebResponse()
        {
            Dispose(false);
        }
    }
}
