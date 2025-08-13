using ETicaret.Domain.Entities.Common;


namespace ETicaret.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public int Quantity { get; set; }
        public ProductType ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
