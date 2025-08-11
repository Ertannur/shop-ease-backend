using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Image : BaseEntity
{
    public string FileName { get; set; }
    public string ImageUrl { get; set; } // Path
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
}