using Microsoft.EntityFrameworkCore;
using trial_api.Models;

namespace trial_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Book>()
            .Property(p => p.Id)
            .IsRequired();
            builder.Entity<Book>()
            .Property(p => p.BookName).HasMaxLength(30)
            .IsRequired();
            builder.Entity<Book>()
            .Property(p => p.Author).HasMaxLength(40)
            .IsRequired();

            builder.Entity<Book>().ToTable("Book");
        }

        public DbSet<Book> Books { get; set; }
    }
}