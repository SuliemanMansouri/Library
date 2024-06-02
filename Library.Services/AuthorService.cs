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


        public async Task<Author> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var author = await db.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return author;
        }

        public async Task<List<Author>> GetList(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var authors = db.Authors.Where(x => x.Name.Contains(name));
            return [.. await authors.ToListAsync()];
        }

        public async Task Save(Author author)
        {
            using var db = _contextFactory.CreateDbContext();
            
            var tmp = db.Authors.FirstOrDefault(x => x.Id == author.Id);
            
            if (tmp == null)
            {
                db.Authors.Add(author);
                await db.SaveChangesAsync();
            }
        }
        public async Task Update(Author author)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Authors.FirstOrDefault(y => y.Id == author.Id);

            if(tmp != null)
            {
                tmp.Email = author.Email;
                tmp.Phone = author.Phone;
                tmp.Name = author.Name;

                await db.SaveChangesAsync();
            }
        }
        public async Task Delete(Author author)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (tmp != null)
            {
                db.Authors.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Author>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return await db.Authors.ToListAsync();
        }
    }
}
