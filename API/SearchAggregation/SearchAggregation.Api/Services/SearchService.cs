using SearchAggregation.Api.Models;

namespace SearchAggregation.Api.Services;


public class SearchService : ISearchService
{
    private readonly IEnumerable<ISearchEngineService> _searchEngineServices;
    private readonly ILogger<SearchService> _logger;

    public SearchService(IEnumerable<ISearchEngineService> searchEngineServices, ILogger<SearchService> logger)
    {
        _searchEngineServices = searchEngineServices;
        _logger = logger;
    }
    
    public async Task<SearchResult> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        var words = query
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Take(20) // assume a limit of 20 words
            .Select(w => w.Trim())
            .Where(w => w.Length <= 150) // assume a limit word length
            .ToArray();
        
        var result = new SearchResult();
        var aggregatorDict = new Dictionary<string, long>();
        try
        {
            var tasks = _searchEngineServices.Select(async service =>
            {
                long sum = 0;
                foreach (var word in words)
                {
                    sum += await service.GetSearchCountAsync(word, cancellationToken);
                }
                lock (aggregatorDict)
                {
                    aggregatorDict[service.Engine] = sum;
                }
            });

            await Task.WhenAll(tasks);
            result.ResultsByProvider = aggregatorDict;
        }
        catch (Exception ex)
        {
            // log exception, handle gracefully
            _logger.LogError(ex, ex.Message);
            result.HasError = true;
            result.ErrorMessage = "One or more providers failed to retrieve data.";
        }
        
        return result;
    }
}