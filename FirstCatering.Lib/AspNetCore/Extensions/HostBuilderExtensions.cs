using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FirstCatering.Lib.AspNetCore.Extensions
{
    /// <summary>
    /// <see cref="IHostBuilder"/> extension helpers
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Configure, build and run the web host
        /// </summary>
        public static void Run<T>(this IHostBuilder host) where T : class
            => host.ConfigureAppConfiguration((context, builder) => builder.Configuration(context))
                .ConfigureWebHostDefaults(builder => builder.UseStartup<T>())
                .Build()
                .Run();
    }
}