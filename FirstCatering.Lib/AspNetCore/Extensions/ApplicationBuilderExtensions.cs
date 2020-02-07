using FirstCatering.Lib.AspNetCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirstCatering.Lib.AspNetCore.Extensions
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension helpers
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// When in development, allow any origin, header and method for cross origin
        /// </summary>
        public static void UseCorsAllowAny(this IApplicationBuilder application)
        {
            var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsDevelopment())
                application.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        }

        /// <summary>
        /// Add exception middleware and enable developer exception page when 
        /// in development
        /// </summary>
        public static void UseException(this IApplicationBuilder application)
        {
            var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsDevelopment())
                application.UseDeveloperExceptionPage();
            application.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// Use Hsts and HttpsRedirection
        /// </summary>
        public static void UseHttps(this IApplicationBuilder application)
        {
            application.UseHsts();
            application.UseHttpsRedirection();
        }
    }
}