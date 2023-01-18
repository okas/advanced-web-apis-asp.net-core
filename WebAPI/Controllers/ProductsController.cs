using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class ProductsController : ControllerBase
{
    private readonly ApiContext _context;

    public ProductsController(ApiContext context)
    {
        _context = context;
    }

    // GET: api/Products
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        List<Product> products = await _context.Products.AsNoTracking().ToListAsync();

        return Ok(products);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int id)
    {
        Product? product = await GetSingleProductAsync(id);

        return product is not null ? Ok(product) : NotFound();
    }

    // PUT: api/Products/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return await ProductExists(id) ? throw ex : NotFound();
        }

        return NoContent();
    }

    // POST: api/Products
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (await GetSingleProductAsync(id) is not { } product)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private async Task<bool> ProductExists(int id)
    {
        return await _context.Products.AsNoTracking().AnyAsync(e => e.Id == id);
    }

    private async Task<Product?> GetSingleProductAsync(int id)
    {
        return await _context.Products.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
    }
}