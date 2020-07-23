using SiteMapper.Core.Models;
using SiteMapper.Data;
using System.Collections.Generic;

namespace SiteMapper.Core
{
    sealed class CreateReport
    {
        public void CreateTextReport(List<IPage> pages, List<IPage> discard)
        {
            List<string> content = new List<string> { "Pages\n" };
            foreach (IPage page in pages)
            {
                content.Add(page.Url);
            }

            content.Add("\nDiscarded Links\n");
            foreach (IPage link in discard)
            {
                content.Add(link.Url);
            }

            Text report = new Text();
            report.Save(content);
        }
    }
}
