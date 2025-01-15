namespace SearchAggregation.Api.Services;

public interface ISearchEngineService
{
    Task<long> GetSearchCountAsync(string query, CancellationToken cancellationToken);
    string Engine { get; }
}