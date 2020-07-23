using System.Net;

namespace SiteMapper.Core.Models
{
    class Page : IPage
    {
        public string Url { get; set; }
        public bool Checked { get; set; } = false;
        public string Body { get; set; }

        public Page(string url)
        {
            Url = url;
            Body = GetHtml(url);
        }

        private string GetHtml(string link)
        {
            WebClient client = new WebClient();
            string result;
            try
            {
                result = client.DownloadString(link);
            }
            catch
            {
                result = link + "Processing Error";
            }
            return result;
        }
    }
}
