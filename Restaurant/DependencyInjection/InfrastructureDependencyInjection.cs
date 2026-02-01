using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IDishCommand, DishCommand>();
            services.AddScoped<IDishQuery, DishQuery>();
            services.AddScoped<ICategoryQuery, CategoryQuery>();

            return services;
        }
    }
}
