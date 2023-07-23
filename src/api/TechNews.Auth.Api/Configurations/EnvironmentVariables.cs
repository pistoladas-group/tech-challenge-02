using System.Text;
using dotenv.net;

namespace TechNews.Auth.Api.Configurations;

public static class EnvironmentVariables
{
    public static string DatabaseConnectionString => "TECHNEWS_CORE_API_DATABASE_CONNECTION_STRING";

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
                // TODO: Configure warning log
                
               throw new ApplicationException("Environment File (.env) not found. The application needs a .env file to run locally. " +
                                                "Please check the section Environment Variables of the README");
            }

            // Ignored if other environments because it is using runtime environment variables
        }

        return services;
    }
}