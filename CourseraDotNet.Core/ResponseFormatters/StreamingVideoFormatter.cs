using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace CourseraDotNet.Core.ResponseFormatters
{
    public class StreamingVideoFormatter : IResponseFormatter<string>
    {
        public string Format(string response)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var urlAttribute = doc.DocumentNode.Descendants("source").First(n => n.Attributes["type"] != null && n.Attributes["type"].Value == "video/mp4").Attributes["src"];
            return urlAttribute == null ? null : urlAttribute.Value;
        }
    }
}
