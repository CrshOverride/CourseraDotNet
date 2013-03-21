using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseraDotNet.Core.Models;
using HtmlAgilityPack;

namespace CourseraDotNet.Core.ResponseFormatters
{
    public class NavigationOptionFormatter : IResponseFormatter<IEnumerable<NavigationOption>>
    {
        public IEnumerable<NavigationOption> Format(string response)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var nodes = doc
                .DocumentNode
                .Descendants("ul")
                .Where(ul => ul.Attributes["class"] != null && ul.Attributes["class"].Value == "course-navbar-list")
                .SelectMany(ul => ul.ChildNodes.Where(n => n.Name == "li").SelectMany(n => n.ChildNodes.Where(cn => cn.Name == "a")));

            /*
            var a = new List<HtmlNode>();

            // LINQ Query not working for some reason, resorting to manual traversal
            foreach (var node in uls)
            {
                var @class = node.Attributes["class"];
                if (@class != null && !string.IsNullOrWhiteSpace(@class.Value) && @class.Value == "course-navbar-list")
                {
                    a.AddRange(node.ChildNodes.Where(n => n.Name == "li").SelectMany(n => n.ChildNodes.Where(inode => inode.Name == "a")));
                }
            }
            */

            return nodes.Select(n => new NavigationOption { Text = n.InnerText.Trim(), Url = n.Attributes["href"].Value });
        }
    }
}
