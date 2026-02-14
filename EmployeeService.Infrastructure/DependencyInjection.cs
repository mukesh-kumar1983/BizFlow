using EmployeeService.Application.Common.Interfaces;
using EmployeeService.Domain.Repositories;
using EmployeeService.Infrastructure.Persistence;
using EmployeeService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BizFlowDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection")));

            // 🔥 IMPORTANT: Register abstraction
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<BizFlowDbContext>());

            // Repository registration
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
