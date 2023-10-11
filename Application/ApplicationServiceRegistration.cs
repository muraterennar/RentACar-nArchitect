using System.Reflection;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Pipelines.Validation;

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

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });



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

