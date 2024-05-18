using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
       
        [MaxLength(60)]
        public string Name { get; set; }
        
        [MaxLength(24)]
        [Phone]
        public string Phone { get; set; }
        
        [MaxLength (60)]
        [EmailAddress]
        public string Email { get; set; }
        
        public List<Book> Books { get; set; }
    }

}
