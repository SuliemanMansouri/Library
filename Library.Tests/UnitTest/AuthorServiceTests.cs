using System.Threading.Tasks;
using Library.Domain.Entities;
using Library.Persistance;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Library.UnitTests.Services
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task Get_AuthorExists_ReturnsAuthor()
        {
            // Arrange
            var testData = new List<Author>
            {
                new() { Id = 1, Name = "Author1" },
                new() { Id = 2, Name = "Author2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Author>>();
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.Provider).Returns(testData.Provider);
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.Expression).Returns(testData.Expression);
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Authors).Returns(mockDbSet.Object);

            var mockContextFactory = new Mock<IDbContextFactory<LibraryContext>>();
            mockContextFactory.Setup(x => x.CreateDbContext()).Returns(mockContext.Object);

            var authorService = new AuthorService(mockContextFactory.Object);

            // Act
            var result = await authorService.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetList_AuthorNameExists_ReturnsListOfAuthors()
        {
            // Arrange
            var testName = "Author";

            var testData = new List<Author>
            {
                new () { Id = 1, Name = "Author1" },
                new () { Id = 2, Name = "Author2" },
                new () { Id = 3, Name = "AnotherAuthor" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Author>>();
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.Provider).Returns(testData.Provider);
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.Expression).Returns(testData.Expression);
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            mockDbSet.As<IQueryable<Author>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Authors).Returns(mockDbSet.Object);

            var mockContextFactory = new Mock<IDbContextFactory<LibraryContext>>();
            mockContextFactory.Setup(x => x.CreateDbContext()).Returns(mockContext.Object);

            var authorService = new AuthorService(mockContextFactory.Object);

            // Act
            var result = await authorService.GetList(testName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.True(result.All(author => author.Name.Contains(testName)));
        }

        // Similar tests can be written for Save, Update, Delete, and GetAll methods
    }
}