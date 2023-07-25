using System.Text;
using dotenv.net;

namespace TechNews.Core.Api.Configurations;

public static class EnvironmentVariables
{
    public static string DatabaseConnectionString => "TECHNEWS_CORE_API_DATABASE_CONNECTION_STRING";
    public static string DiscordWebhookId => "TECHNEWS_CORE_API_DISCORD_WEBHOOK_ID";
    public static string DiscordWebhookToken => "TECHNEWS_CORE_API_DISCORD_WEBHOOK_TOKEN";
    
    public static IServiceCollection AddEnvironmentVariables(this IServiceCollection services, IWebHostEnvironment environment) 
    {
        try
        {
            DotEnv.Fluent()
                .WithExceptions()
                .WithEnvFiles()
                .WithTrimValues()
                .WithEncoding(Encoding.UTF8)
                .WithOverwriteExistingVars()
                .WithProbeForEnv(probeLevelsToSearch: 6)
                .Load();
        }
        catch (Exception)
        {
            if (environment.IsEnvironment("Local"))
            {
                throw new ApplicationException("Environment File (.env) not found. The application needs a .env file to run locally.\nPlease check the section Environment Variables of the README.");
            }

            // Ignored if other environments because it is using runtime environment variables
        }

        return services;
    }
}