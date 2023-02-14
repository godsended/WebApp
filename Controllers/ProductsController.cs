using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private DataContext dataContext;

    public ProductsController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    public IAsyncEnumerable<Product> GetProducts()
    {
        return dataContext.Products.AsAsyncEnumerable();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
    {
        Product? p = await dataContext.Products.FindAsync(id);
        if(p == null)
            return NotFound();
        
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> SaveProduct(ProductBindingTarget product)
    {
        await dataContext.Products.AddAsync(product.ToProduct());
        await dataContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task UpdateProduct(Product product)
    {
        dataContext.Products.Update(product);
        await dataContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task DeleteProduct(long id)
    {
        dataContext.Products.Remove(new (){ProductId = id});
        await dataContext.SaveChangesAsync();
    }
}