using Microsoft.EntityFrameworkCore;
using FirstCatering.Domain;
using FirstCatering.Lib.EntityFramework;

namespace FirstCatering.Data.Company
{
    /// <summary>
    /// Data seed configuration for <see cref="CompanyEntity"/>
    /// </summary>
    public class CompanyEntitySeedConfiguration : IEntitySeedConfiguration
    {
        /// <summary>
        /// Seeds data into the <paramref name="builder"/> for <see cref="CompanyEntity"/>
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> builder</param>
        public void Seed(ModelBuilder builder)
        {
            builder.Entity<CompanyEntity>(x =>
            {
                x.HasData(new
                {
                    Id = 1L,
                    Name = "Bows Formula One High Performance Cars"
                });
            });
        }
    }
}