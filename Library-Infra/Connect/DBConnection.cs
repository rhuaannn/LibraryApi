using Library_Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Library_Infra.Connect
{
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.OwnsOne(e => e.Title)
                      .Property(t => t.Value)
                      .HasColumnName("Title")
                      .IsRequired();

                entity.OwnsOne(e => e.Author)
                      .Property(a => a.Name)
                      .HasColumnName("Author")
                      .IsRequired();

                entity.OwnsOne(e => e.Genre)
                        .Property(g => g.Value)
                        .HasColumnName("Genre")
                        .IsRequired();

                entity.OwnsOne(e => e.PublishedDate)
                        .Property(p => p.Date)
                        .HasColumnName("PublishedDate")
                        .IsRequired();
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBConnection).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
