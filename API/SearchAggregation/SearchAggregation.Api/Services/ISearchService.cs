using SearchAggregation.Api.Models;

namespace SearchAggregation.Api.Services;

public interface ISearchService
{
    Task<SearchResult> SearchAsync(string query, CancellationToken cancellationToken = default);
}