using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace CourseraDotNet.Core.ResponseFormatters
{
    public class ScheduleFormatter : IResponseFormatter<string>
    {
        public string Format(string response)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            return doc.DocumentNode.Descendants("div").First(d => d.Id == "internal_html_page_content").InnerText;
        }
    }
}
