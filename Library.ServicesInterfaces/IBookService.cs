using Library.Domain.Entities;

namespace Library.ServicesInterfaces
{
    public interface IBookService
    {
        void Delete(Book book);
        Book Get(int id);
        Book Get(string isbn);
        List<Book> GetList(string title);
        void Save(Book book);
        void Update(Book book);
        List<Book> GetAll();
    }
}