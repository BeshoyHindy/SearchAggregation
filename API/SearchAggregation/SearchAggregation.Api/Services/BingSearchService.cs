using System.Text.Json;

namespace SearchAggregation.Api.Services;

public class BingSearchService : ISearchEngineService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BingSearchService> _logger;
    public string Engine => "Bing";

    public BingSearchService(HttpClient httpClient, ILogger<BingSearchService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri("https://api.bing.microsoft.com/v7.0/search");
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "YOUR_BING_API_KEY"); // should be from settings
    }

    public async Task<long> GetSearchCountAsync(string query, CancellationToken cancellationToken)
    {
        try
        {
            var url = $"?q={Uri.EscapeDataString(query)}&count=1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BingSearchResponse>(json);

            return result.WebPages.TotalEstimatedMatches;
        }
        catch
        {
            // Log error 
            _logger.LogError("Failed to get search count");
            return 0;
        }
    }

    private record BingSearchResponse(WebPages WebPages);
    private record WebPages(long TotalEstimatedMatches);

}