using TechNews.Auth.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.ConfigureFilters());

builder.Services
        .AddEndpointsApiExplorer()
        .AddEnvironmentVariables(builder.Environment)
        .ConfigureSwagger()
        .ConfigureIdentity()
        .ConfigureDatabase(builder.Configuration)
        .ConfigureDependencyInjections();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHsts();
app.UseHttpsRedirection();
app.UseIdentityConfiguration();
app.MapControllers();
app.Run();
