using eCommerce.Core.Mappers;
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
        return services;
    }
}
