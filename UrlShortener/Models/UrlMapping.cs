namespace UrlShortener.Models
{
    public class UrlMapping
    {
        public string ShortId { get; set; }
        public string LongUrl { get; set; } 
        public string ShortUrl { get; set; } // Full shortened URL

    }
}
