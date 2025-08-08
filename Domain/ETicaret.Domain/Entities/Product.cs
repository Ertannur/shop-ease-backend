using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Category Category { get; set; }      // Enum olarak tanımlayamdım 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
