using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FirstCatering.Domain;

namespace FirstCatering.Data.Login
{
    /// <summary>
    /// Data and migration configuration for <see cref="LoginEntity"/>
    /// </summary>
    public sealed class LoginEntityConfiguration : IEntityTypeConfiguration<LoginEntity>
    {
        /// <summary>
        /// Configures the <paramref name="builder"/> for <see cref="LoginEntity"/>
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{LoginEntity}"/> builder</param>
        public void Configure(EntityTypeBuilder<LoginEntity> builder)
        {
            builder.ToTable("Logins");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.KioskId).IsRequired();
            builder.Property(x => x.Success).IsRequired();
            builder.Property(x => x.DidLock).IsRequired();

            builder.HasOne(x => x.Employee).WithMany(x => x.Logins).HasForeignKey(x => x.EmployeeId);
        }
    }
}