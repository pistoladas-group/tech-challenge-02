using TechNews.Auth.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.ConfigureFilters());

builder.Services
        .AddEndpointsApiExplorer()
        .ConfigureSwagger()
        .ConfigureIdentity()
        .ConfigureDatabase(builder.Configuration);

builder.Services.AddScoped<RsaCrypto>();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHsts();
app.UseHttpsRedirection();
app.UseIdentityConfiguration();
app.MapControllers();
app.Run();
