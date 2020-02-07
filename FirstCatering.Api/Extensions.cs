using System;
using FirstCatering.Data;
using FirstCatering.Lib.AspNetCore.Extensions;
using FirstCatering.Lib.IoC;
using FirstCatering.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FirstCatering.Api
{
    /// <summary>
    /// Api Extension helpers
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Add all First Catering application services into the service collection
        /// </summary>
        public static void AddApplicationServices(this IServiceCollection services)
            => services.AddMatchingInterface(typeof(IEmployeeService).Assembly);

        /// <summary>
        /// Add First Catering EF database context into the service collection
        /// </summary>
        public static void AddDbContext(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(nameof(FirstCateringDbContext));
            services.AddDbContextMigrate<FirstCateringDbContext>(options => options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Add security libs into the service collection
        /// </summary>
        /// <param name="services"></param>
        public static void AddSecurity(this IServiceCollection services)
        {
            services.AddHash(10000, 128);
            services.AddJsonWebToken(Guid.NewGuid().ToString(), TimeSpan.FromMinutes(5));
            services.AddAuthenticationJwtBearer();
        }
    }
}