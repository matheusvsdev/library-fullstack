using BackendLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendLibrary.DataContext
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("tb_category");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("tb_book");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(b => b.Author)
                    .HasColumnName("author")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.HasMany(b => b.Categories)
                    .WithMany(c => c.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "tb_book_category",
                        j => j.HasOne<Category>()
                            .WithMany()
                            .HasForeignKey("category_id"),
                        j => j.HasOne<Book>()
                            .WithMany()
                            .HasForeignKey("book_id")
                );
            });
        }
    }
}