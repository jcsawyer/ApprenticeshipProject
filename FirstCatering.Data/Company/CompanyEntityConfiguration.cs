using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FirstCatering.Domain;

namespace FirstCatering.Data.Company
{
    /// <summary>
    /// Data and migration configuration for <see cref="CompanyEntity"/>
    /// </summary>
    public sealed class CompanyEntityConfiguration : IEntityTypeConfiguration<CompanyEntity>
    {
        /// <summary>
        /// Configures the <paramref name="builder"/> for <see cref="CompanyEntity"/>
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{CompanyEntity}"/> builder</param>
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Employees).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId);
        }
    }
}