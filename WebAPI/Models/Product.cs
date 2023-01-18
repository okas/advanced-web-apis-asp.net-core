using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Sku { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual List<Product>? Products { get; set; }
}