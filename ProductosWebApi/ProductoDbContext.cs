using Microsoft.EntityFrameworkCore;
using ProductosWebApi.Models;

namespace ProductosWebApi
{
    public class ProductoDbContext : DbContext
    {
        public ProductoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

    }
}
