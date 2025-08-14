using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Basket : BaseEntity
{
    public AppUser User { get; set; }
    public Guid UserId { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
    public Order Order { get; set; }
}