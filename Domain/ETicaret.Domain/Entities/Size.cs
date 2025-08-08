using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class Size:BaseEntity
    {
       
        public SizeType SizeType { get; set; }
    }
}
