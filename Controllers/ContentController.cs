using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
    public DataContext dataContext;

    public ContentController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("string")]
    public string GetString() => "This is a string response";

    [HttpGet("object")]

    [Produces("application/json")]
    public async Task<ProductBindingTarget> GetObject()
    {
        Product p = await dataContext.Products.FirstAsync();
        return new ProductBindingTarget()
        {
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId,
            SupplierId = p.SupplierId
        };
    }

    [HttpPost]
    [Consumes("application/json")]
    public string SaveProductJson(ProductBindingTarget product)
    {
        return $"JSON: {product.Name}";
    }

    // [HttpPost]
    // [Consumes("application/xml")]
    // public string SaveProductXml(ProductBindingTarget product)
    // {
    //     return $"XML: {product.Name}";
    // }
}