using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Order : BaseEntity
{
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; }
    public Guid AdressId { get; set; }
    public Adress Adress { get; set; }
}