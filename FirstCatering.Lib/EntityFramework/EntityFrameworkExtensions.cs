using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace FirstCatering.Lib.EntityFramework
{
    /// <summary>
    /// Entity framework extension helpers
    /// </summary>
    public static class EntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Applies EF model configurations using reflection to discover implementations of
        /// <see cref="IEntityTypeConfiguration{TEntity}"/>
        /// </summary>
        /// <param name="modelBuilder">EF model builder</param>
        public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder)
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var configuration in types.Select(Activator.CreateInstance))
                modelBuilder.ApplyConfiguration((dynamic)configuration);
        }

        /// <summary>
        /// Applies EF seed data using reflection to discover implementations of
        /// <see cref="IEntitySeedConfiguration{TEntity}"/>
        /// </summary>
        /// <param name="builder">EF model builder</param>
        public static void SeedFromAssembly(this ModelBuilder builder)
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(t => t == typeof(IEntitySeedConfiguration))).ToList();

            foreach (IEntitySeedConfiguration configuration in types.Select(Activator.CreateInstance))
                configuration.Seed(builder);
        }
    }
}