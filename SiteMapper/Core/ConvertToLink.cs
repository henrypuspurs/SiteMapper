using SiteMapper.Core.Models;
using System.Text.RegularExpressions;

namespace SiteMapper.Core
{
    public class ConvertToLink
    {
        public string Href { get; set; }
        public string Origin { get; set; }

        public ConvertToLink(string link, string source)
        {
            Href = link;
            Origin = source;
        }

        public Link FullProcess()
        {
            return true switch
            {
                bool _ when new Regex(@"^(http)").IsMatch(Href) => FullAddressConvertToLink(Href),
                bool _ when new Regex(@".+?").IsMatch(Href) => PartialAddressConvertToLink(Href),
                _ => new Link { Valid = false },
            };
        }

        public Link FullAddressConvertToLink(string link)
        {
            if (new Regex($@"^{Origin}").IsMatch(link))
            {
                return CreateLinkFromFullAddress(link, LinkSource.Internal);
            }
            else
            {
                return CreateLinkFromFullAddress(link, LinkSource.External);
            }
        }

        public Link PartialAddressConvertToLink(string link)
        {
            string fullUrl;

            if (new Regex(@"^\.\.").IsMatch(link))
            {
                fullUrl = Regex.Replace(StripForwardslashLeadAndTail(Origin), @"\b/.+", "/", RegexOptions.RightToLeft) + StripForwardslashLeadAndTail(Regex.Replace(link, @"\.\.", ""));
                return CreateLinkFromFullAddress(fullUrl, LinkSource.Internal);
            }
            else if (new Regex(@"^/|^\w|\.html.?$").IsMatch(link))
            {
                link = StripForwardslashLeadAndTail(link);
                fullUrl = Origin + link;
                return CreateLinkFromFullAddress(fullUrl, LinkSource.Internal);
            }
            else
            {
                return new Link { Url = link, Valid = false, LinkSource = LinkSource.Internal };
            }
        }

        public Link CreateLinkFromFullAddress(string Url, LinkSource linkSource)
        {
            if (new Regex(@"(\.html)$|/$").IsMatch(Url))
            {
                return new Link { Url = Url, Valid = true, LinkType = LinkType.Page, LinkSource = linkSource };
            }
            else
            {
                return new Link { Url = Url, Valid = true, LinkType = LinkType.File, LinkSource = linkSource };
            }
        }

        public string StripForwardslashLeadAndTail(string link)
        {
            string result = Regex.Replace(link, @"^\/|\/$", "");
            return result;
        }
    }
}
