using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.ExpenseDate)
                   .IsRequired();

            builder.Property(x => x.CategoryId)
                   .IsRequired();

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.HasIndex(x => new { x.UserId, x.ExpenseDate });

            builder.HasIndex(x => x.CategoryId);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
