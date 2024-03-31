using Microsoft.AspNetCore.Mvc.Testing;

namespace ElectionsTest;

public class Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Tests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Vote")]
    [InlineData("/Results")]
    [InlineData("/api/voters/Putin")]
    [InlineData("/api/voters/Macron")]
    [InlineData("/api/voters/Biden")]
   
    public async Task Test1(string url)
    {
        var client = _factory.CreateClient();
        
        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

}