using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SearchAggregation.Api.Models;
using SearchAggregation.Api.Services;

namespace SearchAggregation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly IMemoryCache _cache;

    public SearchController(ISearchService searchService, IMemoryCache cache)
    {
        _searchService = searchService;
        _cache = cache;
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] SearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
            return BadRequest("Search query is required");

        if (_cache.TryGetValue(request.Query, out var cachedResults))
            return Ok(cachedResults);

        var results = await _searchService.SearchAsync(request.Query);
        _cache.Set(request.Query, results, TimeSpan.FromMinutes(5));

        return Ok(results);
    }
}