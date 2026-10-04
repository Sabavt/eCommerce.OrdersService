using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    { 
        services.AddValidatorsFromAssemblyContaining<DependencyInjection>();
        services.AddAutoMapper(p => p.AddProfile());
        return services;
    }
}
