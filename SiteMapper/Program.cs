using SiteMapper.Core;
using System;

namespace SiteMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            MapSite mapSite = new MapSite();
            mapSite.Start("http://www.enteryoursitehere.com/");
        }
    }
}
