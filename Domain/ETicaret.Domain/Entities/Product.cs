using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Category Category { get; set; }      // Enum olarak tanımlayamdım 
        public string Name { get; set; }
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }
    }
}
