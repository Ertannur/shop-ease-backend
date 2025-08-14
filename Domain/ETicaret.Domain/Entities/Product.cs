using ETicaret.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
