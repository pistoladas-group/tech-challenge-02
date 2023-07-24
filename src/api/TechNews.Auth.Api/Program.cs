using TechNews.Auth.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.ConfigureFilters());

builder.Services
        .AddEndpointsApiExplorer()
        .ConfigureSwagger()
        .AddEnvironmentVariables(builder.Environment)
        .AddLoggingConfiguration(builder.Host)
        .ConfigureIdentity()
        .ConfigureDatabase()
        .ConfigureDependencyInjections();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHsts();
app.UseHttpsRedirection();
app.UseIdentityConfiguration();
app.MapControllers();
app.Run();
