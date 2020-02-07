using Microsoft.Extensions.DependencyInjection;
using System;
using FirstCatering.Lib.Logging;
using FirstCatering.Lib.Security.Jwt;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Scrutor;
using Microsoft.EntityFrameworkCore;
using FirstCatering.Lib.Security.Hashing;

namespace FirstCatering.Lib.IoC
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension helpers
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds logging to the service collection
        /// </summary>
        public static void AddLogger(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddSingleton<ILogger>(_ => new Logger(configuration));
        }

        /// <summary>
        /// Adds json web tokens to the service collection using the given <paramref name="key"/>
        /// and expiration <paramref name="expires"/>
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="expires">Expiration time</param>
        public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires)
            => services.AddJsonWebToken(new JsonWebTokenSettings(key, expires));

        /// <summary>
        /// Adds json web tokens to the service collection using the given <paramref name="key"/>,
        /// expiration <paramref name="expires"/>, <paramref name="audience"/> and <paramref name="issuer"/>
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="expires">Expiration time</param>
        /// <param name="audience">Token audience</param>
        /// <param name="issuer">Token issuer</param>
        public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires, string audience, string issuer)
            => services.AddJsonWebToken(new JsonWebTokenSettings(key, expires, audience, issuer));

        /// <summary>
        /// Adds json web tokens to the service collection using the given <paramref name="jsonWebTokenSettings"/>
        /// configuration
        /// </summary>
        /// <param name="jsonWebTokenSettings"><see cref="JsonWebTokenSettings"/> token configuration</param>
        public static void AddJsonWebToken(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings)
        {
            services.AddSingleton<IJsonWebTokenSettings>(_ => jsonWebTokenSettings);
            services.AddSingleton<IJsonWebToken, JsonWebToken>();
        }

        /// <summary>
        /// Adds an EF database context for in-memory storage
        /// </summary>
        /// <typeparam name="T"><see cref="DbContext"/> context type</typeparam>
        public static void AddDbContextMemory<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddDbContextPool<T>(options => options.UseInMemoryDatabase(typeof(T).Name));
            services.BuildServiceProvider().GetRequiredService<T>().Database.EnsureCreated();
        }

        /// <summary>
        /// Adds EF database migration to the service collection with the specified
        /// <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T"><see cref="DbContext"/> context type</typeparam>
        /// <param name="options"><see cref="DbContextOptions{T}"/> context options</param>
        public static void AddDbContextMigrate<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> options ) where T: DbContext
        {
            services.AddDbContextPool<T>(options);
            services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();
        }

        /// <summary>
        /// Adds hashing to the service collection using the specified <paramref name="iteration"/>
        /// and <paramref name="size"/>
        /// </summary>
        /// <param name="iteration">Number of hashing iterations</param>
        /// <param name="size">Size of hash</param>
        public static void AddHash(this IServiceCollection services, int iteration, int size)
            => services.AddSingleton<IHash>(_ => new Hash(iteration, size));

        /// <summary>
        /// Dynamically adds classes matching interfaces for the specified <paramref name="assemblies"/>
        /// to the service collection
        /// </summary>
        /// <param name="assemblies"><see cref="Assembly[]"/> assemblies to add</param>
        public static void AddMatchingInterface(this IServiceCollection services, params Assembly[] assemblies)
            => services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());
    }
}