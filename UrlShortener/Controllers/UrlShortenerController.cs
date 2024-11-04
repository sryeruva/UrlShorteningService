using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : Controller
    {
        #region Fields
        private readonly UrlShorteningService _urlShorteningService;
        #endregion

        #region Constructor
        public UrlShortenerController(UrlShorteningService urlShorteningService)
        {
            _urlShorteningService = urlShorteningService;
        }

        #endregion

        #region Public Methods

        [HttpPost("shorten")]
        public IActionResult ShortenUrl([FromBody] string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
            {
                return BadRequest("Long URL cannot be empty.");
            }

            if (!IsValidUrl(longUrl))
            {
                return BadRequest("Invalid URL format.");
            }

            var mapping = _urlShorteningService.ShortenUrl(longUrl);
            return CreatedAtAction(nameof(GetLongUrl), new { short_Id = mapping.ShortId }, mapping);
        }

        [HttpGet("{short_Id}")]
        public IActionResult GetLongUrl(string short_Id)
        {
            var mapping = _urlShorteningService.GetLongUrl(short_Id);
            if (mapping != null)
            {
                return Ok(new { LongUrl = mapping.LongUrl });
            }
            return NotFound(new { error = "404 Not Found" });
        }

        #endregion

        #region Private Metods
        private bool IsValidUrl(string url)
        {
            // Check if the URL is well-formed
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        #endregion
    }
}
