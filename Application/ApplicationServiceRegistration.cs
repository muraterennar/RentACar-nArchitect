using System.Reflection;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Serilog;
using Core.CrossCuttingConcerns.Serilog.Logger;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Burada Sistemime eklediğimiz Herbir Business Rules (İş Kurallarını) teker Teker eklemkten kaçınıp
        // Tipini verdiğimiz Türün Assembly Eklemimizi Sağlıyor.
        services.AddSubClassedOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        // Fluient Validation ICo Ekleniyor Assmpley aranarak
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // MediatR servisini projenin yürütülen derlemesine kaynak olarak ekler.
        services.AddMediatR(configuration =>
        {
            // Uygulamanın yürütülen derlemesinden hizmetleri kaydeder.
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            // İstek doğrulama davranışını MediatR servisine ekler.
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));

            // İşlem kapsamı davranışını MediatR servisine ekler.
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));

            // Önbellek davranışını MediatR servisine ekler.
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));

            // Önbellek kaldırma davranışını MediatR servisine ekler.
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));

            // Günlükleme davranışını MediatR servisine ekler.
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

        });

        // LoggerServiceBase türünde bir nesne, FileLogger | MsSQLLogger türünde bir nesne ile birlikte servislere ekleniyor.
        //services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<LoggerServiceBase, MsSqlLogger>();



        return services;
    }

    // Tipini verdiğimiz Türün Assembly Eklemimizi Sağlayan Fonksiyon.
    public static IServiceCollection AddSubClassedOfType(this IServiceCollection services, Assembly assembly, Type type, Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);


        return services;
    }
}

