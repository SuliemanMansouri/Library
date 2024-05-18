using Library.Domain.Entities;
using Library.Persistance;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService(IDbContextFactory<LibraryContext> dbContextFactory) : IBookService
    {
        private readonly IDbContextFactory<LibraryContext> _contextFactory = dbContextFactory;

        public void Save(Book book)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Books.FirstOrDefault(x => x.Id == book.Id);
            if (tmp == null)
            {
                db.Books.Add(book);
                db.SaveChanges();
            }

        }

        public void Update(Book book)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Books.FirstOrDefault(x => x.Id == book.Id);
            if (tmp != null)
            {
                tmp.Title = book.Title;
                tmp.ISBN = book.ISBN;
                tmp.NumberOfPages = book.NumberOfPages;
                tmp.Language = book.Language;

                db.SaveChanges();
            }
        }

        public void Delete(Book book)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Books.FirstOrDefault(x => x.Id == book.Id);
            if (tmp != null)
            {
                db.Books.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Book Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var book = db.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        public Book Get(string isbn)
        {
            using var db = _contextFactory.CreateDbContext();

            var book = db.Books.FirstOrDefault(x => x.ISBN == isbn);
            return book;
        }

        public List<Book> GetList(string title)
        {
            using var db = _contextFactory.CreateDbContext();

            var books = db.Books.Where(x => x.Title.Contains(title));
            return [.. books];
        }
    }
}
