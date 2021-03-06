﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Phone
{
    public class PhoneWebResponse : IWebResponse
    {
        private readonly WebResponse _response;

        public IDictionary<string, IEnumerable<string>> Headers
        {
            get
            {
                return _response.Headers.AllKeys.ToDictionary(k => k, k => (new List<string> { _response.Headers[k] }) as IEnumerable<string>);
            }
        }

        public PhoneWebResponse(WebResponse response)
        {
            _response = response;
        }

        public Task<Stream> GetResponseStreamAsync()
        {
            return Task<Stream>.Factory.StartNew(() => _response.GetResponseStream());
        }

        public void Dispose()
        {
            
        }
    }
}
