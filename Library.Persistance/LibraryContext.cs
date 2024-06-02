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
                .HasMaxLength (20);

            modelBuilder.Entity<Book>()
                .Property(x => x.NumberOfPages)
                .HasColumnType("smallint");

            modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "John Doe", Phone = "123-456-7890", Email = "johndoe@example.com" },
            new Author { Id = 2, Name = "Jane Smith", Phone = "234-567-8901", Email = "janesmith@example.com" },
            new Author { Id = 3, Name = "Alice Johnson", Phone = "345-678-9012", Email = "alicejohnson@example.com" },
            new Author { Id = 4, Name = "Bob Brown", Phone = "456-789-0123", Email = "bobbrown@example.com" },
            new Author { Id = 5, Name = "Charlie Davis", Phone = "567-890-1234", Email = "charliedavis@example.com" }
            // Add more authors if needed...
        );

            // Seed books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Book One", ISBN = "978-3-16-148410-0", NumberOfPages = 320, Language = "English" },
                new Book { Id = 2, Title = "Book Two", ISBN = "978-1-56619-909-4", NumberOfPages = 250, Language = "Spanish" },
                new Book { Id = 3, Title = "Book Three", ISBN = "978-0-12345-678-9", NumberOfPages = 150, Language = "French" },
                new Book { Id = 4, Title = "Book Four", ISBN = "978-0-54321-987-6", NumberOfPages = 450, Language = "German" },
                new Book { Id = 5, Title = "Book Five", ISBN = "978-1-23456-789-0", NumberOfPages = 200, Language = "English" },
                new Book { Id = 6, Title = "Book Six", ISBN = "978-1-11111-222-2", NumberOfPages = 180, Language = "Spanish" },
                new Book { Id = 7, Title = "Book Seven", ISBN = "978-2-22222-333-3", NumberOfPages = 220, Language = "French" },
                new Book { Id = 8, Title = "Book Eight", ISBN = "978-3-33333-444-4", NumberOfPages = 300, Language = "German" },
                new Book { Id = 9, Title = "Book Nine", ISBN = "978-4-44444-555-5", NumberOfPages = 320, Language = "English" },
                new Book { Id = 10, Title = "Book Ten", ISBN = "978-5-55555-666-6", NumberOfPages = 400, Language = "Spanish" },
                new Book { Id = 11, Title = "Book Eleven", ISBN = "978-6-66666-777-7", NumberOfPages = 150, Language = "French" },
                new Book { Id = 12, Title = "Book Twelve", ISBN = "978-7-77777-888-8", NumberOfPages = 450, Language = "German" },
                new Book { Id = 13, Title = "Book Thirteen", ISBN = "978-8-88888-999-9", NumberOfPages = 200, Language = "English" },
                new Book { Id = 14, Title = "Book Fourteen", ISBN = "978-9-99999-000-0", NumberOfPages = 180, Language = "Spanish" },
                new Book { Id = 15, Title = "Book Fifteen", ISBN = "978-1-01010-101-1", NumberOfPages = 220, Language = "French" },
                new Book { Id = 16, Title = "Book Sixteen", ISBN = "978-2-02020-202-2", NumberOfPages = 300, Language = "German" },
                new Book { Id = 17, Title = "Book Seventeen", ISBN = "978-3-03030-303-3", NumberOfPages = 320, Language = "English" },
                new Book { Id = 18, Title = "Book Eighteen", ISBN = "978-4-04040-404-4", NumberOfPages = 400, Language = "Spanish" },
                new Book { Id = 19, Title = "Book Nineteen", ISBN = "978-5-05050-505-5", NumberOfPages = 150, Language = "French" },
                new Book { Id = 20, Title = "Book Twenty", ISBN = "978-6-06060-606-6", NumberOfPages = 450, Language = "German" }
            );

            // Seed author-book relationships
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithMany(b => b.Authors)
                .UsingEntity(j => j.HasData(
                    new { AuthorsId = 1, BooksId = 1 },
                    new { AuthorsId = 1, BooksId = 2 },
                    new { AuthorsId = 2, BooksId = 3 },
                    new { AuthorsId = 2, BooksId = 4 },
                    new { AuthorsId = 3, BooksId = 5 },
                    new { AuthorsId = 3, BooksId = 6 },
                    new { AuthorsId = 4, BooksId = 7 },
                    new { AuthorsId = 4, BooksId = 8 },
                    new { AuthorsId = 5, BooksId = 9 },
                    new { AuthorsId = 5, BooksId = 10 },
                    new { AuthorsId = 1, BooksId = 11 },
                    new { AuthorsId = 2, BooksId = 12 },
                    new { AuthorsId = 3, BooksId = 13 },
                    new { AuthorsId = 4, BooksId = 14 },
                    new { AuthorsId = 5, BooksId = 15 },
                    new { AuthorsId = 1, BooksId = 16 },
                    new { AuthorsId = 2, BooksId = 17 },
                    new { AuthorsId = 3, BooksId = 18 },
                    new { AuthorsId = 4, BooksId = 19 },
                    new { AuthorsId = 5, BooksId = 20 }
                ));

        }
    }
}
