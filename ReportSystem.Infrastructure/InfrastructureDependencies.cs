using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportSystem.Domain.Entities.UserEntity;
using ReportSystem.Domain.IRepository.IUnitOfWork;
using ReportSystem.Infrastructure.Context;
using ReportSystem.Infrastructure.Repository.UnitOfWork;

namespace ReportSystem.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnectionString")));
            services.AddIdentity<User, AspNetRole>()
                .AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
