using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;
using CourseraDotNet.Core.Models;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Services
{
    public class GetCoursesService : IService<IEnumerable<Course>> 
    {
        private const string Url = "https://www.coursera.org/maestro/api/topic/list_my";
        private readonly Session _session;

        public GetCoursesService(Session session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Course>> ExecuteAsync()
        {
            var response = await _session.GetPageAsync(Url, HttpVerb.Get);
            return JsonConvert.DeserializeObject<List<Course>>(response);
        }
    }
}
