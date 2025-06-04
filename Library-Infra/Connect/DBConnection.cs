using Library_Domain.Model;
using Library_Domain.ValueObject;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_Infra.Connect
{
    public class DBConnection : IdentityDbContext
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
                     .Property(a => a.Value)
                     .HasColumnName("Author")
                     .IsRequired();


                entity.OwnsOne(e => e.Genre)
                        .Property(g => g.Value)
                        .HasColumnName("Genre")
                        .IsRequired();

          
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBConnection).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
