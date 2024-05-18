using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistance
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) :
            base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired(true);

            modelBuilder.Entity<Book>()
                .Property(x=>x.ISBN)
                .HasMaxLength (12);

            modelBuilder.Entity<Book>()
                .Property(x => x.NumberOfPages)
                .HasColumnType("smallint");
            

        }
    }
}
