namespace SearchAggregation.Api.Models;

public class SearchResult
{
    public Dictionary<string, long> ResultsByProvider { get; set; } = new Dictionary<string, long>();
    public bool HasError { get; set; }
    public string? ErrorMessage { get; set; }
}