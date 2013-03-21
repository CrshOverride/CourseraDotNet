namespace CourseraDotNet.Core.Core
{
    public enum HttpVerb
    {
        Post,
        Get,
        Put,
        Delete
    }

    public static class HttpVerbExtensions
    {
        public static string ToVerbString(this HttpVerb verb)
        {
            switch (verb)
            {
                case HttpVerb.Delete:
                    return "DELETE";
                case HttpVerb.Get:
                    return "GET";
                case HttpVerb.Post:
                    return "POST";
                case HttpVerb.Put:
                    return "PUT";
            }

            return "";
        }
    }
}