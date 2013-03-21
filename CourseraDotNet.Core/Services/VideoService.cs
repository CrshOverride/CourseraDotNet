using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Core.Services
{
    public class VideoService : IService<Stream>
    {
        private readonly string _url;
        private readonly Session _session;

        public VideoService(string url, Session session)
        {
            _url = url;
            _session = session;
        }
        public async Task<Stream> ExecuteAsync()
        {
            return await _session.GetVideoAsync(_url, HttpVerb.Get);
        }
    }
}
