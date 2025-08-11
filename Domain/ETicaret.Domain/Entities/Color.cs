using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;
namespace ETicaret.Domain.Entities
{
    public class Color:BaseEntity
    {
        public ColorType ColorType { get; set; }
        public ProductType ProductType { get; set; }
    }
}
