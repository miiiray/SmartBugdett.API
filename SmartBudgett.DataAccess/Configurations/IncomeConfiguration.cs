using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Configurations
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.HasKey(i => i.Id);

            // Finansal verilerde hassasiyet (Precision/Scale) tanımı zorunludur
            builder.Property(i => i.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(i => i.IncomeDate)
                   .IsRequired();

            builder.Property(i => i.CategoryId)
                   .IsRequired();

            builder.Property(i => i.UserId)
                   .IsRequired();

            builder.HasIndex(i => new { i.UserId, i.IncomeDate });

            builder.HasIndex(i => i.CategoryId);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(i => i.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(i => i.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
