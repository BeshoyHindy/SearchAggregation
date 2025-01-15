using System.Text.Json;

namespace SearchAggregation.Api.Services;

public class GoogleSearchService : ISearchEngineService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BingSearchService> _logger;
    public string Engine => "Google";

    public GoogleSearchService(HttpClient httpClient, ILogger<BingSearchService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri("https://www.googleapis.com/customsearch/v1");
    }

    public async Task<long> GetSearchCountAsync(string query, CancellationToken cancellationToken)
    {
        try
        {
            var apiKey = "GOOGLE_API_KEY"; // should be from settings
            var cx = "CUSTOM_SEARCH_ENGINE_ID"; // should be from settings
            var url = $"?q={Uri.EscapeDataString(query)}&key={apiKey}&cx={cx}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GoogleSearchResponse>(json);

            return long.Parse(result.SearchInformation.TotalResults);
        }
        catch
        {
            // Log error
            _logger.LogError("Failed to get search count");
            return 0;
        }
    }
    private record GoogleSearchResponse(SearchInformation SearchInformation);
    private record SearchInformation(string TotalResults);

}