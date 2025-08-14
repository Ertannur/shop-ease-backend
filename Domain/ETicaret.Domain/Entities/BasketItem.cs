using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class BasketItem : BaseEntity
{
    public  Basket Basket { get; set; }
    public Guid BasketId { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid ProductTypeId { get; set; }
}