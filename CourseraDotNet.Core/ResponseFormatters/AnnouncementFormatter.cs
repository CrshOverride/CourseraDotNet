using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseraDotNet.Core.Models;
using HtmlAgilityPack;

namespace CourseraDotNet.Core.ResponseFormatters
{
    public class AnnouncementFormatter : IResponseFormatter<IEnumerable<Announcement>>
    {
        public IEnumerable<Announcement> Format(string response)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var divs = doc
                .DocumentNode
                .Descendants("h2")
                .Where(n => n.InnerText.Trim() == "Announcements")
                .SelectMany(n => n.ParentNode.ChildNodes.Where(cn => cn.Name == "div").Select(cn => cn.FirstChild.NextSibling));

            return divs.Select(n => new Announcement
                {
                    Body = n.ChildNodes.First(cn => cn.Name == "div").InnerText.Trim(),
                    Date = n.ChildNodes.Where(cn => cn.Name == "div").Skip(1).First().InnerText.Trim(),
                    Title = n.ChildNodes.First(cn => cn.Name == "h3").InnerText.Trim()
                });
        }
    }
}
