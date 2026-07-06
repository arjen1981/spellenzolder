using GameCatalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameCatalog.Infrastructure.Sql;

public sealed class GameCatalogDbContext(DbContextOptions<GameCatalogDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
    }
}

internal sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(g => g.Platform)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.RegistrationDate)
            .IsRequired();

        builder.OwnsOne(g => g.Condition, condition =>
        {
            condition.Property(c => c.Booklet).HasColumnName("ConditionBooklet").IsRequired();
            condition.Property(c => c.Box).HasColumnName("ConditionBox").IsRequired();
            condition.Property(c => c.Media).HasColumnName("ConditionMedia").IsRequired();
        });

        builder.HasIndex(g => g.Platform);
        builder.HasIndex(g => g.Name);
    }
}
