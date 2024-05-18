using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public string Language { get; set; }
        public List<Author> Authors { get; set; }
    }
}
