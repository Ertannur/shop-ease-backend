using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    
    public ICollection<Product> Products { get; set; }
}