using ETicaret.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public int Quantity { get; set; }

        public ProductType ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
