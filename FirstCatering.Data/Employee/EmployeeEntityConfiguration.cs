using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FirstCatering.Domain;

namespace FirstCatering.Data.Employee
{
    /// <summary>
    /// Data and migration configuration for <see cref="EmployeeEntity"/>
    /// </summary>
    public sealed class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        /// <summary>
        /// Configures the <paramref name="builder"/> for <see cref="EmployeeEntity"/>
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{EmployeeEntity}"/> builder</param>
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired().HasMaxLength(16);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.MobileNumber).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();
            builder.Property(x => x.PIN).IsRequired();
            builder.Property(x => x.Balance).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasOne(x => x.Company).WithMany(x => x.Employees).HasForeignKey(x => x.CompanyId);

            builder.HasMany(x => x.Logins).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.Transactions).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
        }
    }
}