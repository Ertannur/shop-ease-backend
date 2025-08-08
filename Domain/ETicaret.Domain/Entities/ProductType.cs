using ETicaret.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class ProductType:BaseEntity
    {
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Color Color { get; set; }
        public Guid ColorId { get; set; }
        public Size Size { get; set; }
        public Guid SizeId { get; set; }
        public Stock Stock { get; set; }
    }
}
