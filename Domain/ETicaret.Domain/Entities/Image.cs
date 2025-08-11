using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Image : BaseEntity
{
    public string ImageUrl { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
}