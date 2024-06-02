using Library.Domain.Entities;

namespace Library.ServicesInterfaces
{
    public interface IAuthorService
    {
        Task Delete(Author author);
        Task<Author> Get(int id);
        Task<List<Author>> GetList(string name);
        Task<List<Author>> GetAll();
        Task Save(Author author);
        Task Update(Author author);
    }
}
