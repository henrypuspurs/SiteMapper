using SiteMapper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace SiteMapper.Core
{
    public class ParseLinks
    {
        public string SourcePage { get; set; }
        public List<IPage> InvalidList;
        public List<IPage> Webpage;

        public ParseLinks(List<IPage> webpage, string sourcePage)
        {
            SourcePage = sourcePage;
            Webpage = webpage;
            InvalidList = new List<IPage>();
            Parser();
        }

        private void Parser()
        {
            IEnumerable<IPage> tempList;
            tempList = Webpage.ToList();

            foreach (IPage page in tempList.Where(c => !c.Checked))
            {
                Console.WriteLine($"\nProcessing {page.Url}");
                MatchCollection matches = Regex.Matches(page.Body, @"(?<=href="")[^\?].*?(?="")");
                AddToPageLists(MatchCollectionToStringList(matches), Webpage, InvalidList);
                page.Checked = true;
            }
        }

        private void AddToPageLists(List<string> rawLinks, List<IPage> rootPage, List<IPage> invalidPage)
        {
            foreach (string link in rawLinks)
            {
                var processedLink = new ConvertToLink(link, SourcePage).FullProcess();
                Console.WriteLine($"{processedLink.Url}");
                if (processedLink.Valid && processedLink.LinkType == LinkType.Page && processedLink.LinkSource == LinkSource.Internal)
                {
                    var validPage = new Page(processedLink.Url) { Checked = false };
                    rootPage.Add(validPage);
                }
                else
                {
                    var invalid = new Page(processedLink.Url) { Checked = false };
                    invalidPage.Add(invalid);
                }
                Thread.Sleep(1000);
            }
        }

        private List<string> MatchCollectionToStringList(MatchCollection matches)
        {
            var rawLinks = new List<string>();
            foreach (Match match in matches)
            {
                rawLinks.Add(match.Value);
            }
            return rawLinks.Distinct().ToList();
        }

        
    }
}
