using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class Color:BaseEntity
    {
        public ColorType ColorType { get; set; }
        public ProductType ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
        //istenirse hex şeklinde orneğin FFFF gibi siyah değerler verilebilir
    }
}
