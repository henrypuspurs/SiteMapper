namespace SiteMapper.Core.Models
{
    public class Link
    {
        public string Url;
        public bool Valid = true;
        public LinkSource LinkSource { get; set; }
        public LinkType LinkType { get; set; }
    }
    public enum LinkSource
    {
        Internal,
        External
    }
    public enum LinkType
    {
        Page,
        File
    }
}
