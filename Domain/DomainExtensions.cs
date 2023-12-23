using System;
using Domain.Base;
using Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services,
            Func<IServiceProvider, BaseSqlConnection> connectionOptions)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDepartmentCommand).Assembly))
                           .AddTransient<BaseSqlConnection>(connectionOptions);
        }
    }
}