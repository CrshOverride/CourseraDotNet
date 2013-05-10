using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseraDotNet.Core.Core
{
    public interface IWebResponse : IDisposable
    {
        IDictionary<string, IEnumerable<string>> Headers { get; }
        Task<Stream> GetResponseStreamAsync();
    }
}
