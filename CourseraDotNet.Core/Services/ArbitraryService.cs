using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;
using CourseraDotNet.Core.Models;
using CourseraDotNet.Core.ResponseFormatters;

namespace CourseraDotNet.Core.Services
{
    public class ArbitraryService<T> : IService<T>
    {
        private readonly IResponseFormatter<T> _formatter;
        private readonly string _url;
        private readonly Session _session;
        private readonly Course _course;

        public ArbitraryService(Course course, string url, IResponseFormatter<T> formatter, Session session)
        {
            _url = url;
            _formatter = formatter;
            _session = session;
            _course = course;
        }

        public async Task<T> ExecuteAsync()
        {
            if (!_session.IsAuthRedirected)
            {
                var url = string.Format("{0}auth/auth_redirector?type=login&subtype=normal",
                                        _course.CourseDetails.First().HomeLink);
                await _session.GetPageAsync(url, HttpVerb.Get);
            }

            var response = await _session.GetPageAsync(_url, HttpVerb.Get);
            return _formatter.Format(response);
        }
    }
}
