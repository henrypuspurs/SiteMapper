using MoreLinq;
using SiteMapper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SiteMapper.Core
{
    public class MapSite
    {
        string sourcePage;
        List<IPage> PageList = new List<IPage>();
        List<IPage> DiscardList = new List<IPage>();
        public void Start(string website)
        {
            sourcePage = website;
            PageList.Add(new Page(website));
            Loop();
            CreateReport report = new CreateReport();
            report.CreateTextReport(PageList, DiscardList);
        }

        private void Loop()
        {
            while (PageList.Any(l => !l.Checked))
            {
                Console.Write($"\rRunning {sourcePage}: Page List has {PageList.Count} objects, Discard List has {DiscardList.Count} objects.");
                var parser = new ParseLinks(PageList, sourcePage);
                PageList.AddRange(parser.Webpage);
                DiscardList.AddRange(parser.InvalidList);
                foreach (IPage page in PageList)
                {
                    Regex.Replace(page.Url, @"/$", "");
                }
                PageList = PageList.DistinctBy(u => u.Url).ToList();
                Console.Clear();
            }
        }
    }
}
