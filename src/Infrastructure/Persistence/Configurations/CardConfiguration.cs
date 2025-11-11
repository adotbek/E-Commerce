using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(c => c.CardId);

        builder.Property(c => c.CardNumber)
            .IsRequired();

        builder.Property(c => c.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(c => c.User)
            .WithMany() 
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Payments)
            .WithOne(p => p.Card)
            .HasForeignKey(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
