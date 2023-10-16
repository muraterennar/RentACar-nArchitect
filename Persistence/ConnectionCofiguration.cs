using System;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public static class ConnectionCofiguration
{
    public static string ConnectionString
    {
        get
        {
            // Çalışma ortamını al (örneğin: Development, Production)
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Yapılandırma yöneticisini oluştur
            ConfigurationManager configurationManager = new ConfigurationManager();

            //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "~/RentACar/WebAPI"));,

            // Temel yolunu belirle (appsettings.json dosyasının bulunduğu dizin)
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "/Users/muraterennar/Projects/rentACarDemo/projects/RentACar/WebAPI"));

            // appsettings.json dosyasını yükle (zorunlu, değiştirildiğinde yeniden yükle)
            configurationManager.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Çalışma ortamına özgü ayarları yükle (isteğe bağlı)
            configurationManager.AddJsonFile($"appsettings.{environment}.json", optional: true);

            // Ortam değişkenlerini de yükle
            configurationManager.AddEnvironmentVariables();

            // "RentACarDb" adlı bağlantı dizesini al ve geri döndür
            return configurationManager.GetConnectionString("RentACarDb");
        }
    }
}

