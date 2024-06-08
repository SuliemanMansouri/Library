using Library.Domain.Entities;
using Library.Persistance;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.IntegrationTests
{
    public class BookServiceTests
    {
        private DbContextOptions<LibraryContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private IDbContextFactory<LibraryContext> GetDbContextFactory(DbContextOptions<LibraryContext> options)
        {
            var mockFactory = new Mock<IDbContextFactory<LibraryContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(() => new LibraryContext(options));
            return mockFactory.Object;
        }

        [Fact]
        public async Task Save_ShouldAddBook()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages= 500, Language="Arabic" };

            // Act
            await service.Save(book);

            // Assert
            using var context = new LibraryContext(options);
            var savedBook = await context.Books.FirstOrDefaultAsync(b => b.Title == "Book1");
            Assert.NotNull(savedBook);
        }

        [Fact]
        public async Task GetById_ShouldReturnBookById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages = 500, Language = "Arabic" };
            await service.Save(book);

            // Act
            var fetchedBook = await service.Get(book.Id);

            // Assert
            Assert.NotNull(fetchedBook);
            Assert.Equal(book.Title, fetchedBook.Title);
        }

        [Fact]
        public async Task GetByISBN_ShouldReturnBookByISBN()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages = 500, Language = "Arabic" };
            await service.Save(book);

            // Act
            var fetchedBook = await service.Get("1234567890");

            // Assert
            Assert.NotNull(fetchedBook);
            Assert.Equal(book.ISBN, fetchedBook.ISBN);
        }

        [Fact]
        public async Task GetList_ShouldReturnBooksByTitle()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            await service.Save(new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages= 500, Language="Arabic" });
            await service.Save(new Book { Title = "Book2", ISBN = "0987654321", NumberOfPages = 500, Language = "Arabic" });

            // Act
            var books = await service.GetList("Book");

            // Assert
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllBooks()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            await service.Save(new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages= 500, Language="Arabic" });
            await service.Save(new Book { Title = "Book2", ISBN = "0987654321", NumberOfPages = 500, Language = "Arabic" });

            // Act
            var books = await service.GetAll();

            // Assert
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public async Task Delete_ShouldRemoveBook()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages = 500, Language = "Arabic" };
            await service.Save(book);

            // Act
            await service.Delete(book);

            // Assert
            using var context = new LibraryContext(options);
            var deletedBook = await context.Books.FindAsync(book.Id);
            Assert.Null(deletedBook);
        }

        [Fact]
        public async Task Update_ShouldModifyBook()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book { Title = "Book1", ISBN = "1234567890", NumberOfPages = 500, Language = "Arabic" };
            await service.Save(book);

            // Act
            book.Title = "Updated Book";
            book.ISBN = "0987654321";
            await service.Update(book);

            // Assert
            using var context = new LibraryContext(options);
            var updatedBook = await context.Books.FindAsync(book.Id);
            Assert.Equal("Updated Book", updatedBook.Title);
            Assert.Equal("0987654321", updatedBook.ISBN);
        }

        [Fact]
        public async Task AddAuthorToBook_ShouldAddAuthor()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book {Title = "Book1", ISBN = "1234567890", NumberOfPages = 500, Language = "Arabic" };
            var author = new Author { Name = "Author1", Phone = "123456789", Email = "author@company.com" };
            await service.Save(book);

            // Act
            await service.AddAuthorToBook(book, author);

            // Assert
            using var context = new LibraryContext(options);
            var savedBook = await context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == book.Id);
            Assert.NotNull(savedBook);
            Assert.Contains(savedBook.Authors, a => a.Name == "Author1");
        }

        [Fact]
        public async Task RemoveAuthorFromBook_ShouldRemoveAuthor()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BookService(factory);
            var book = new Book {Title = "Book1", ISBN = "1234567890", NumberOfPages = 500, Language = "Arabic" };
            var author = new Author { Name = "Author1", Phone = "123456789", Email = "author@company.com" };
            await service.Save(book);
            await service.AddAuthorToBook(book, author);

            // Act
            await service.RemoveAuthorFromBook(book, author);

            // Assert
            using var context = new LibraryContext(options);
            var savedBook = await context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == book.Id);
            Assert.NotNull(savedBook);
            Assert.DoesNotContain(savedBook.Authors, a => a.Name == "Author1");
        }
    }
}
