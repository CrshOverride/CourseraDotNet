using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseraDotNet.Core.Core;

namespace CourseraDotNet.Core.Services
{
    public interface IService<T>
    {
        Task<T> ExecuteAsync();
    }
}
