using TechNews.Core.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.ConfigureFilters());

builder.Services
        .AddEndpointsApiExplorer()
        .ConfigureSwagger()
        .AddEnvironmentVariables(builder.Environment)
        .AddLoggingConfiguration(builder.Host)
        .ConfigureDatabase()
        .ConfigureDependencyInjections()
        .AddAuthConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseAuthConfiguration();
app.MapControllers();
app.Run();
