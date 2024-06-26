﻿using Library.Domain.Entities;
using Library.Persistance;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IDbContextFactory<LibraryContext> _contextFactory;

        public BookService(IDbContextFactory<LibraryContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task Save(Book book)
        {
            using var db = _contextFactory.CreateDbContext();
            
            var tmp = db.Books.FirstOrDefault(x => x.Id == book.Id);
            
            if (tmp == null)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
            }

        }

        public async Task Update(Book book)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Books.FirstOrDefault(x => x.Id == book.Id);
            
            if (tmp != null)
            {
                tmp.Title = book.Title;
                tmp.ISBN = book.ISBN;
                tmp.NumberOfPages = book.NumberOfPages;
                tmp.Language = book.Language;

                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Book book)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Books.FirstOrDefault(x => x.Id == book.Id);
            
            if (tmp != null)
            {
                db.Books.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Book> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<Book> Get(string isbn)
        {
            using var db = _contextFactory.CreateDbContext();

            var book = await db.Books.FirstOrDefaultAsync(x => x.ISBN.ToUpper() == isbn.Trim().ToUpper());
            return book;
        }

        public async Task<List<Book>> GetList(string title)
        {
            using var db = _contextFactory.CreateDbContext();

            var books = await db.Books.Where(x => x.Title.Contains(title)).ToListAsync();
            return [.. books];
        }

        public async Task<List<Book>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return [.. await db.Books.Include(x=>x.Authors).ToListAsync()];
        }

        public async Task AddAuthorToBook(Book book, Author author)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpBook = db.Books.Include(x=>x.Authors).FirstOrDefault(x=>x.Id == book.Id);
            if (tmpBook != null)
            {
                var tmpAuthor = db.Authors.FirstOrDefault(x=>x.Id ==author.Id); 
                if (tmpAuthor != null)
                {
                    tmpBook.Authors.Add(tmpAuthor);
                }
                else
                {
                    db.Authors.Add(author);
                    await db.SaveChangesAsync();
                    tmpBook.Authors.Add(author);
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveAuthorFromBook(Book book, Author author)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpBook = db.Books.Include(x=>x.Authors).FirstOrDefault(x=>x.Id == book.Id);
            if (tmpBook != null)
            {
                var bookAuthor = tmpBook.Authors.FirstOrDefault(x=>x.Id == author.Id);
                if (bookAuthor != null)
                {
                    tmpBook.Authors.Remove(bookAuthor);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
