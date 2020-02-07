using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FirstCatering.Data
{
    /// <summary>
    /// FirstCatering db context factory during design-time building service
    /// </summary>
    public sealed class ContextFactory : IDesignTimeDbContextFactory<FirstCateringDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <remarks>
        /// This method is used in generating data migrations.
        /// It uses a hard-coded SQL connection string for a Dockerised instance
        /// of SQL server that should not exist in production
        /// </remarks>
        /// <param name="args"> Arguments provided by the design-time service. </param>
        /// <returns> An instance of <typeparamref name="TContext" />. </returns>
        public FirstCateringDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FirstCateringDbContext>();

            builder.UseSqlServer("Server=127.0.0.1,1433;Database=FirstCatering;User Id=sa;Password=Sup3rStr0ng1;");
            builder.EnableSensitiveDataLogging();

            return new FirstCateringDbContext(builder.Options);
        }
    }
}