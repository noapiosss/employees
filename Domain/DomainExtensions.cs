using Domain.Base;
using Domain.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDepartmentCommand).Assembly))
                           .AddScoped<BaseSqlConnection>();
        }
    }
}