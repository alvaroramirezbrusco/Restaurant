using Application.Behaviors;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Restaurant.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.Load("Application");

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(assembly)
            );

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );

            return services;
        }
    }
}
