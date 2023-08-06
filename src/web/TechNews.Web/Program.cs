using TechNews.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
        .AddHttpClient()
        .AddEnvironmentVariables(builder.Environment)
        .AddControllersWithViews(options => options.Filters.AddFilterConfiguration());

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
