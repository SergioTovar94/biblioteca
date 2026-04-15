using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences;


public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    #region DbSets 
    public DbSet<BookEntity> BooksEntities => Set<BookEntity>();
    public DbSet<AuthorEntity> AuthorEntities => Set<AuthorEntity>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure BookEntity
        modelBuilder.Entity<BookEntity>(e =>
        {
            e.ToTable("Books");
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.PublishedDate).IsRequired();
            e.Property(x => x.Genre).HasMaxLength(50).IsRequired(false);
            e.Property(x => x.NumberOfPages).IsRequired();
            e.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId);
        });

        // Configure AuthorEntity
        modelBuilder.Entity<AuthorEntity>(e =>
        {
            e.ToTable("Authors");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.Property(x => x.BirthDate).IsRequired(false);
            e.Property(x => x.Country).HasMaxLength(100).IsRequired(false);
            e.Property(x => x.Biography).HasColumnType("nvarchar(max)").IsRequired(false);
            e.Property(x => x.IsDeleted).HasDefaultValue(false);
            e.HasMany(x => x.Books).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId);
        });
    }
}