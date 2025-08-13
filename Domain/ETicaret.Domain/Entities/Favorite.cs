using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class Favorite
    {
       
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
