using Library.Domain.Entities;

namespace Library.ServicesInterfaces
{
    public interface IAuthorService
    {
        void Delete(Author author);
        Author Get(int id);
        List<Author> GetList(string name);
        List<Author> GetAll();
        void Save(Author author);
        void Update(Author author);
    }
}
