using Library.Domain.Entities;
using Library.Persistance;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Library.Tests.IntegrationTests
{
    public class AuthorServiceTests
    {
        private readonly IDbContextFactory<LibraryContext> _factory;

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
        public async Task Save_ShouldAddAuthor()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new AuthorService(factory);
            var author = new Author { Name = "Author1", Phone = "123456789", Email = "author@company.com" };

            // Act
            await service.Save(author);

            // Assert
            using var context = new LibraryContext(options);
            var savedAuthor = await context.Authors.FirstOrDefaultAsync(a => a.Name == "Author1");
            Assert.NotNull(savedAuthor);
        }

        [Fact]
        public async Task Get_ShouldReturnAuthorById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new AuthorService(factory);
            var author = new Author { Name = "Author1", Phone = "123456789", Email = "author@company.com" };
            await service.Save(author);

            // Act
            var fetchedAuthor = await service.Get(author.Id);

            // Assert
            Assert.NotNull(fetchedAuthor);
            Assert.Equal(author.Name, fetchedAuthor.Name);
        }

        [Fact]
        public async Task GetList_ShouldReturnAuthorsByName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new AuthorService(factory);
            await service.Save(new Author { Name = "Author1", Phone = "123456789", Email = "author@company.com" });
            await service.Save(new Author { Name = "Author2", Phone = "123456789", Email = "author@company.com" });

            // Act
            var authors = await service.GetList("Author");

            // Assert
            Assert.Equal(2, authors.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAuthors()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new AuthorService(factory);
            await service.Save(new Author {Name = "Author1", Phone = "123456789", Email = "author@company.com" });
            await service.Save(new Author {Name = "Author2", Phone = "123456789", Email = "author@company.com" });
            await service.Save(new Author {Name = "Author2", Phone = "123456789", Email = "author@company.com" });

            // Act
            var authors = await service.GetAll();

            // Assert
            Assert.Equal(3, authors.Count);
        }

        [Fact]
        public async Task Delete_ShouldRemoveAuthor()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new AuthorService(factory);
            var author = new Author {Name = "Author1", Phone = "123456789", Email = "author@company.com" };
            await service.Save(author);

            // Act
            await service.Delete(author);

            // Assert
            using var context = new LibraryContext(options);
            var deletedAuthor = await context.Authors.FindAsync(author.Id);
            
            deletedAuthor.ShouldBe(null);

        }

        [Fact]
        public async Task Update_ShouldModifyAuthor()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new AuthorService(factory);
            var author = new Author { Name = "Author1", Email = "author1@example.com", Phone = "1234567890" };
            await service.Save(author);

            // Act
            author.Name = "Updated Author";
            author.Email = "updatedauthor@example.com";
            author.Phone = "0987654321";
            await service.Update(author);

            // Assert
            using var context = new LibraryContext(options);
            var updatedAuthor = await context.Authors.FindAsync(author.Id);
            Assert.Equal("Updated Author", updatedAuthor.Name);
            Assert.Equal("updatedauthor@example.com", updatedAuthor.Email);
            Assert.Equal("0987654321", updatedAuthor.Phone);
        }


    }
}
