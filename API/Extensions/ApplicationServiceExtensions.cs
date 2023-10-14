using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt => 
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(typeof(Application.Todos.List.Handler).Assembly));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}