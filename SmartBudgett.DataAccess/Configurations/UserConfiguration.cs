using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBudgett.Entities;

namespace SmartBudgett.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Password)
                   .IsRequired();
        }
    }
}