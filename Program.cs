using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<DataContext>(opts => 
{
    opts.UseNpgsql(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
