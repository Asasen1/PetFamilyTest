using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.ComplexProperty(u => u.Email,
            emailBuilder => { emailBuilder.Property(e => e.Value).HasColumnName("email"); });

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.HasOne(u => u.Role).WithMany();
    }
}