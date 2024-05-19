using Library.Domain.Entities;
using Library.Persistance;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IDbContextFactory<LibraryContext> _contextFactory;

        public AuthorService(IDbContextFactory<LibraryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public Author Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var author = db.Authors.FirstOrDefault(x => x.Id == id);
            return author;
        }

        public List<Author> GetList(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var authors = db.Authors.Where(x => x.Name.Contains(name));
            return [.. authors];
        }

        public void Save(Author author)
        {
            using var db = _contextFactory.CreateDbContext();
            
            var tmp = db.Authors.FirstOrDefault(x => x.Id == author.Id);
            
            if (tmp == null)
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
        }
        public void Update(Author author)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Authors.FirstOrDefault(y => y.Id == author.Id);

            if(tmp != null)
            {
                tmp.Email = author.Email;
                tmp.Phone = author.Phone;
                tmp.Name = author.Name;

                db.SaveChanges();
            }
        }
        public void Delete(Author author)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (tmp != null)
            {
                db.Authors.Remove(tmp);
                db.SaveChanges();
            }
        }

        public List<Author> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return db.Authors.ToList();
        }
    }
}
