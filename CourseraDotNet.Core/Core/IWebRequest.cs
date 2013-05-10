using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseraDotNet.Core.Core
{
    public interface IWebRequest : IDisposable
    {
        Task SetContentAsync(string content);
        Task<IWebResponse> GetResponseAsync();

        Uri RequestUri { get; }
        string ContentType { get; set; }
        string Method { get; set; }
    }
}
