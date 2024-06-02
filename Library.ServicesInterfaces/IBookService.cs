using Library.Domain.Entities;

namespace Library.ServicesInterfaces
{
    public interface IBookService
    {
        Task Delete(Book book);
        Task<Book> Get(int id);
        Task<Book> Get(string isbn);
        Task<List<Book>> GetList(string title);
        Task Save(Book book);
        Task Update(Book book);
        Task<List<Book>> GetAll();
        Task AddAuthorToBook(Book book, Author author);
        Task RemoveAuthorFromBook(Book book, Author author);
    }
}