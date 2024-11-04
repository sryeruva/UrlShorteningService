using System;
using System.Collections.Concurrent;
using UrlShortener.Services;
using Xunit;


//Git code checkin
public class UrlShorteningServiceTests
{
    private readonly UrlShorteningService _service;

    public UrlShorteningServiceTests()
    {
        _service = new UrlShorteningService();
    }

    [Fact]
    public void ShortenUrl_ValidUrl_ReturnsMapping()
    {
        // Arrange
        var longUrl = "https://www.example.com";

        // Act
        var mapping = _service.ShortenUrl(longUrl);

        // Assert
        Assert.NotNull(mapping);
        Assert.Equal(longUrl, mapping.LongUrl);
        Assert.NotNull(mapping.ShortId);
        Assert.StartsWith("http://yourdomain.com/", mapping.ShortUrl);
    }

    [Fact]
    public void GetLongUrl_ValidShortId_ReturnsLongUrl()
    {
        // Arrange
        var longUrl = "https://www.example.com";
        var mapping = _service.ShortenUrl(longUrl);

        // Act
        var result = _service.GetLongUrl(mapping.ShortId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(longUrl, result.LongUrl);
    }

    [Fact]
    public void GetLongUrl_InvalidShortId_ReturnsNull()
    {
        // Act
        var result = _service.GetLongUrl("invalid_id");

        // Assert
        Assert.Null(result);
    }

   
}
