using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FirstCatering.Domain;

namespace FirstCatering.Data.Transaction
{
    /// <summary>
    /// Data and migration configuration for <see cref="CompanyEntity"/>
    /// </summary>
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        /// <summary>
        /// Configures the <paramref name="builder"/> for <see cref="TransactionEntity"/>
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{TransactionEntity}"/> builder</param>
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.Amount).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.PreviousBalance).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasOne(x => x.Employee).WithMany(x => x.Transactions).HasForeignKey(x => x.EmployeeId);
        }
    }
}