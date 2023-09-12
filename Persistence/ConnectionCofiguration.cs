using System;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public static class ConnectionCofiguration
{
    public static string ConnectionString
    {
        get
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            ConfigurationManager configurationManager = new();
            //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "~/RentACar/WebAPI"));
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "/Users/muraterennar/Projects/rentACarDemo/projects/RentACar/WebAPI"));
            configurationManager.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configurationManager.AddJsonFile($"appsettings.{environment}.json", optional: true);
            configurationManager.AddEnvironmentVariables();

            return configurationManager.GetConnectionString("RentACarDb");
        }
    }
}

