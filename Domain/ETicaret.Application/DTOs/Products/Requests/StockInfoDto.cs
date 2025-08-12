using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Application.DTOs.Products.Requests
{
    public class StockInfoDto
    {
        public int ColorId { get; set; }      // Enum: ColorType
        public int SizeId { get; set; }       // Enum: SizeType
        public int Quantity { get; set; }     // Stok adedi
    }
}
