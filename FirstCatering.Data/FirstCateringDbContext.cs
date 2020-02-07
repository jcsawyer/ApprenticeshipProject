using Microsoft.EntityFrameworkCore;
using FirstCatering.Lib.EntityFramework;

namespace FirstCatering.Data
{
    /// <summary>
    /// First Catering EF database context
    /// </summary>
    public sealed class FirstCateringDbContext : DbContext
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{FirstCateringDbContext}"/> options</param>
        public FirstCateringDbContext(DbContextOptions<FirstCateringDbContext> options) : base(options) { }

        /// <summary>
        /// Configure models to be constructed into the context
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> model builder for db context</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly();
            builder.SeedFromAssembly();
        }
    }
}