using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;
}