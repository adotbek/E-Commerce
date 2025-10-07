namespace Domain.Entities;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public long? ParentCategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Category? ParentCategory { get; set; }
    public ICollection<Category>? SubCategories { get; set; }
    public ICollection<Product>? Products { get; set; }
}
