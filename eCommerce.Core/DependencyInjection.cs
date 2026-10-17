using eCommerce.Core.Mappers;
using eCommerce.Core.RabbitMQ;
using eCommerce.Core.RabbitMQ.Contracts;
using eCommerce.Core.RabbitMQ.HostedServices;
using eCommerce.Core.Redis;
using eCommerce.Core.ServiceContracts;
using eCommerce.Core.Services;
using eCommerce.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    { 
        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
        services.AddAutoMapper(p => {
            p.AddProfile(new OrderMappingProfile());
            p.AddProfile(new OrderItemMappingProfile()); 
        });
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddStackExchangeRedisCache(opt => opt.Configuration = 
        $"{Environment.GetEnvironmentVariable("REDIS_HOST")}:{Environment.GetEnvironmentVariable("REDIS_PORT")}");
        services.AddSingleton<RedisCacheService>();
        services.AddSingleton<IRabbitMQProductUpdationConsumer, RabbitMQProductUpdationConsumer>();
        services.AddSingleton<IRabbitMQDeletionConsumer, RabbitMQDeletionConsumer>();
        services.AddSingleton<IRabbitMQAdderConsumer, RabbitMQProductAdderConsumer>();
        services.AddHostedService<RabbitMQUpdationConumerHostedService>();
        services.AddHostedService<RabbitMQDeletionConumerHostedService>();
        services.AddHostedService<RabbitMQAdderConsumerHost>();

        return services;
    }
} 