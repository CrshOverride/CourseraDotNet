using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CourseraDotNet.Core.Models;
using HtmlAgilityPack;

namespace CourseraDotNet.Core.ResponseFormatters
{
    public class LectureGroupFormatter : IResponseFormatter<IEnumerable<LectureGroup>>
    {
        public IEnumerable<LectureGroup> Format(string response)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var groups = doc
                .DocumentNode
                .Descendants("div")
                .First(d => d.Attributes["class"] != null && d.Attributes["class"].Value == "course-item-list")
                .ChildNodes
                .Where(n => n.Name == "div")
                .Select(n => new LectureGroup
                    {
                        Name = WebUtility.HtmlDecode(n.ChildNodes.First(cn => cn.Name == "h3").InnerText).Trim(),
                        Lectures = n.NextSibling.ChildNodes.Where(cn => cn.Name == "li").Select(cn => new Lecture
                            {
                                IsViewed = cn.Attributes["class"] != null && cn.Attributes["class"].Value == "viewed",
                                Title = WebUtility.HtmlDecode(cn.ChildNodes.First(ccn => ccn.Name == "a").FirstChild.InnerText).Trim(),
                                ProtectedVideoUrl = cn.ChildNodes.First(ccn => ccn.Name == "div").ChildNodes.First(ccn => ccn.Attributes["title"] != null && ccn.Attributes["title"].Value == "Video (MP4)").Attributes["href"].Value,
                                IframeUrl = cn.ChildNodes.First(ccn => ccn.Name == "a").Attributes["data-modal-iframe"].Value,
                                PdfUrl = cn.ChildNodes.FirstOrDefault(ccn => ccn.Name == "div") == null
                                    ? null
                                    : cn.ChildNodes.First(ccn => ccn.Name == "div").ChildNodes.FirstOrDefault(ccn => ccn.Attributes["title"] != null && ccn.Attributes["title"].Value == "Slides") == null
                                        ? null
                                        : cn.ChildNodes.First(ccn => ccn.Name == "div").ChildNodes.First(ccn => ccn.Attributes["title"] != null && ccn.Attributes["title"].Value == "Slides").Attributes["href"].Value
                            })
                    });

            return groups;
        }
    }
}
