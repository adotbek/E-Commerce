using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Hash)
            .IsRequired();

        builder.Property(u => u.Salt)
            .IsRequired();

        builder.Property(u => u.GoogleId)
            .HasMaxLength(200);

        builder.Property(u => u.ProfileImgUrl)
            .HasMaxLength(500);

        builder.Property(u => u.TelegramId)
            .IsRequired(false);

        builder.HasOne(u => u.Role)
            .WithMany() 
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Card)
            .WithOne(c => c.User) 
            .HasForeignKey<User>(u => u.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Confirmer)
            .WithMany() 
            .HasForeignKey(u => u.ConfirmerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User) 
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User) 
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Carts)
            .WithOne(c => c.User) 
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
