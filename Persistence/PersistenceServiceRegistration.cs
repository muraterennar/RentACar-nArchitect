﻿using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("rentACarDb"));

        // Vari Tabanı Bağlantısını Tanımlıyoruz
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RentACarDb")));

        // Repository Sınıflarını IoC'ye injekte ediyorux
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<IFuelRepository, FuelRepository>();
        services.AddScoped<ITransmissionRepository, TransmissionRepository>();

        return services;
    }
}

