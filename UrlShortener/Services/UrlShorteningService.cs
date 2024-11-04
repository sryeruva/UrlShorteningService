using System.Collections.Concurrent;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlShorteningService
    {
        #region Fields
        private readonly ConcurrentDictionary<string, UrlMapping> _urlMappings = new();
        private const string BaseUrl = "http://yourdomain.com/";
        #endregion

        #region Public Methods
        public UrlMapping ShortenUrl(string longUrl)
        {
            var shortId = GenerateShortId(longUrl);
            var mapping = new UrlMapping { ShortId = shortId, LongUrl = longUrl, ShortUrl = $"{BaseUrl}{shortId}" };
            _urlMappings.TryAdd(shortId, mapping);
            return mapping;
        }

        public UrlMapping GetLongUrl(string shortId)
        {
            if (_urlMappings.TryGetValue(shortId, out var mapping))
            { 
                return mapping;
            }
            return null; // Not found
        }

        #endregion

        #region Private Methods

        private string GenerateShortId(string longUrl)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(longUrl)).Substring(0, 6);
        }

        #endregion

    }
}
