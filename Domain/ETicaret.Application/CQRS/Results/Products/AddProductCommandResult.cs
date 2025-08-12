using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Application.CQRS.Results.Products
{
    public class AddProductCommandResult
    {
        public bool Success {get; set; }
        public string Message {get; set; }
    }
}
