using SearchAggregation.Api.Services;

namespace SearchAggregation.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();  
        builder.Services.AddMemoryCache();
        
        builder.Services.AddHttpClient<GoogleSearchService>();
        builder.Services.AddHttpClient<BingSearchService>();
        
        builder.Services.AddHttpClient<ISearchEngineService, GoogleSearchService>();
        builder.Services.AddHttpClient<ISearchEngineService, BingSearchService>();
        builder.Services.AddScoped<ISearchService, SearchService>();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Customize your Swagger UI endpoint or title here if you like
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My .NET 9 API v1");
                options.DocumentTitle = "My .NET 9 Swagger UI";
            });
        }

        app.UseHttpsRedirection();
        
        app.MapControllers();

        app.Run();
    }
}