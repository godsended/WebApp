using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private DataContext dataContext;

    public SuppliersController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("{id}")]
    public async Task<Supplier?> GetSupplier(long id)
    {
        Supplier? supplier = await dataContext.Suppliers
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.SupplierId == id);

        if(supplier?.Products != null)
        {
            foreach(Product p in supplier.Products)
                p.Supplier = null;
        }

        return supplier;
    }

    [HttpPatch("{id}")]
    public async Task<Supplier?> PatchSupplier(long id, JsonPatchDocument<Supplier> patchDocument)
    {
        Supplier? s = await dataContext.Suppliers.FindAsync(id);
        if(s != null)
        {
            patchDocument.ApplyTo(s);
            await dataContext.SaveChangesAsync();
        }
        return s;
    }
}