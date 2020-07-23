namespace SiteMapper.Core.Models
{
    public interface IPage
    {
        bool Checked { get; set; }
        string Url { get; set; }
        string Body { get; set; }
    }
}