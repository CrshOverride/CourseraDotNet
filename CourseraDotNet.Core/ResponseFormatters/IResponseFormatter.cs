using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseraDotNet.Core.ResponseFormatters
{
    // T can't be covariant. Windows Phone 7 won't be able to load the assembly.
    // http://connect.microsoft.com/VisualStudio/feedback/details/654003/windows-phone-7-cannot-load-assemblies-containing-certain-delegate-declarations
    // ReSharper disable TypeParameterCanBeVariant
    public interface IResponseFormatter<T>
    // ReSharper restore TypeParameterCanBeVariant
    {
        T Format(string response);
    }
}
