using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;
using CourseraDotNet.Core.Models;
using CourseraDotNet.Core.ResponseFormatters;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Services
{
    public class GetCourseOptionsService : IService<IEnumerable<NavigationOption>>
    {
        private readonly Course _course;
        private readonly Session _session;
        private readonly string _url;
        private readonly NavigationOptionFormatter _formatter = new NavigationOptionFormatter();

        public GetCourseOptionsService(Course course, Session session)
        {
            _course = course;
            _session = session;
            _url = string.Format("{0}auth/auth_redirector?type=login&subtype=normal",
                                 _course.CourseDetails.First().HomeLink);

            _session.IsAuthRedirected = true;
        }

        public async Task<IEnumerable<NavigationOption>> ExecuteAsync()
        {
            var response = await _session.GetPageAsync(_url, HttpVerb.Get);
            return _formatter.Format(response);
        }
    }
}
