using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FirstCatering.Lib.AspNetCore.Extensions
{
    /// <summary>
    /// <see cref="IConfigurationBuilder"/> extension helpers
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// Add json and environment variable sources to configuration builder
        /// </summary>
        /// <param name="context"><see cref="HostBuilderContext"/> builder context</param>
        public static void Configuration(this IConfigurationBuilder builder, HostBuilderContext context)
            => builder
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json")
                .AddEnvironmentVariables();
    }
}