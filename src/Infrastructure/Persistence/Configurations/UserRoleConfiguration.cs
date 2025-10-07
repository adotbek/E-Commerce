using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).HasMaxLength(50).IsRequired();
        builder.Property(r => r.Description).HasMaxLength(255);

        builder.HasData(
            new Role { Id = 1, Name = "SuperAdmin", Description = "." },
            new Role { Id = 2, Name = "Admin", Description = "." },
            new Role { Id = 3, Name = "User", Description = "." }
        );
    }

}
