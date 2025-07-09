using ekaterina_anisimova_kt_42_22_April_25.Services; 
using Microsoft.Extensions.DependencyInjection; 

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<ILoadService, LoadService>();

            return services;
        }
    }
}