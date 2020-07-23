using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SiteMapper.Data
{
    class Text
    {
        public void Save(List<string> report)
        {
            string title = Regex.Match(report[0], @"\.(.*)\.").Groups[1].Value;
            string content = string.Join("\n", report);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"Report_{title}.txt", content);
        }
    }
}
